using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PocketApiV3.Persistence
{
    public class SyncEngine : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pocketClient">The IPocketClient to use.  Note that it
        /// will not be disposed of by this class.</param>
        public SyncEngine(IPocketClient pocketClient, SyncEngineSettings settings)
        {
            _pocketClient = pocketClient;
            _settings = settings;
            _syncDbContext = new Data.SyncDbContext();
        }

        public bool IsInitialized => _isInitializeCompleted;
        public bool IsSynchronizing => _isSyncInProgress;

        public void Dispose() => Dispose(true);

        public async Task Initialize(CancellationToken cancellationToken)
        {
            if (_isInitializeStarted.TrySet(true))
            {
                try
                {
                    await _syncDbContext.Database.MigrateAsync(cancellationToken);
                    _isInitializeCompleted.Set(true);
                }
                catch
                {
                    _isInitializeStarted.Set(false);
                    throw;
                }
            }
        }

        public async Task<StatisticsResponse> GetStatistics(CancellationToken cancellationToken)
        {
            var total = await _syncDbContext.Items.CountAsync(cancellationToken);
            var read = await _syncDbContext.Items.CountAsync(x => x.TimeRead != null, cancellationToken);
            var result = new StatisticsResponse
            {
                CountAll = total,
                CountRead = read,
                CountUnread = total - read
            };
            return result;
        }

        public Task PersistRequest(AddRequest addRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task PersistRequest(ModifyRequest modifyRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RetrieveResponse> PersistRequest(RetrieveRequest retrieveRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<SyncResult> SynchronizeAsync(CancellationToken cancellationToken)
        {
            EnsureIsInitialized();
            var syncResultBuilder = new SyncResult.Builder();
            if (_isSyncInProgress.TrySet(true))
            {
                return SynchronizeAsyncCore(syncResultBuilder, cancellationToken);
            }
            else
            {
                syncResultBuilder.Code = SyncResultCode.SyncAlreadyInProgress;
                var result = syncResultBuilder.Build();
                return Task.FromResult(result);
            }
        }

        async Task<SyncResult> SynchronizeAsyncCore(SyncResult.Builder syncResultBuilder, CancellationToken cancellationToken)
        {
            // Get latest state variables from DB
            var stateVars = LoadSyncEngineStateVariables();

            var retrieveRequest = new RetrieveRequest()
            {
                Count = SyncPageSize,
                DetailType = RetrieveDetailType.Complete,
                Since = stateVars.LastSyncStartTime,
                State = RetrieveState.All
            };

            RetrieveResponse retrieveResponse = null;

            while (retrieveResponse == null || retrieveResponse.IsComplete == false)
            {
                retrieveResponse = await _pocketClient.SendRequest(retrieveRequest);

                foreach (var retrieveResponseItem in retrieveResponse.Items)
                {
                    var pocketItem = _syncDbContext.Items.Find(retrieveResponseItem.Key);
                    if (pocketItem == null)
                    {
                        pocketItem = new Data.Models.PocketItem();
                        pocketItem.CopyFrom(retrieveResponseItem.Value);
                        _syncDbContext.Items.Add(pocketItem);
                    }
                    else
                    {
                        pocketItem.CopyFrom(retrieveResponseItem.Value);
                    }
                }

                retrieveRequest.NextPage(SyncPageSize);
            }

            _syncDbContext.SaveChanges();

            stateVars.LastSyncStartTime = syncResultBuilder.StartTime;
            SaveSyncEngineStateVariables(stateVars);

            syncResultBuilder.Code = SyncResultCode.Completed;
            var syncResult = syncResultBuilder.Build();
            return syncResult;
        }


        #region Non-Public Members

        readonly IPocketClient _pocketClient;
        readonly SyncEngineSettings _settings;
        AtomicBoolean _isInitializeStarted;
        AtomicBoolean _isInitializeCompleted;
        AtomicBoolean _isSyncInProgress;

        Data.SyncDbContext _syncDbContext;

        const int SyncPageSize = 200;
        const int SyncEngineStateVariablesSingletonId = 1;

        protected Data.Models.SyncEngineStateVariables LoadSyncEngineStateVariables()
        {
            using (var context = new Data.SyncDbContext())
            {
                var result = context.SyncEngineStateVariables.Find(SyncEngineStateVariablesSingletonId);
                if (result == null)
                {
                    result = new Data.Models.SyncEngineStateVariables() { Id = SyncEngineStateVariablesSingletonId };
                    context.SyncEngineStateVariables.Add(result);
                    context.SaveChanges();
                }
                return result;
            }
        }

        protected void SaveSyncEngineStateVariables(Data.Models.SyncEngineStateVariables syncEngineStateVariables)
        {
            syncEngineStateVariables.Id = SyncEngineStateVariablesSingletonId;
            using (var context = new Data.SyncDbContext())
            {
                context.Entry(syncEngineStateVariables).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Interlocked.Exchange(ref _syncDbContext, null)?.Dispose();
            }
        }

        protected void EnsureIsInitialized()
        {
            if (!_isInitializeCompleted)
                throw new InvalidOperationException($"The {nameof(SyncEngine)} has not been initialized.");
        }

        #endregion
    }
}

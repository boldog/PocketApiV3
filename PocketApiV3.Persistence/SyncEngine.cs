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
        readonly IPocketClient _pocketClient;
        readonly SyncEngineSettings _settings;
        AtomicBoolean _isInitializeStarted;
        AtomicBoolean _isInitializeCompleted;

        Data.SyncDbContext _syncDbContext;
        SemaphoreSlim _syncLock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pocketClient">The IPocketClient to use.  Note that it
        /// will not be disposed of by this class.</param>
        public SyncEngine(IPocketClient pocketClient, SyncEngineSettings settings)
        {
            _pocketClient = pocketClient;
            _settings = settings;
            _syncLock = new SemaphoreSlim(1);
            _syncDbContext = new Data.SyncDbContext();
        }

        public bool IsInitialized => _isInitializeCompleted;
        public bool IsSynchronizing => _syncLock.CurrentCount == 0;

        public void Dispose() => Dispose(true);

        public void Initialize()
        {
            if (_isInitializeStarted.TrySet(true))
            {
                try
                {
                    _syncDbContext.Database.Migrate();
                    _isInitializeCompleted.Set(true);
                }
                catch
                {
                    _isInitializeStarted.Set(false);
                    throw;
                }
            }
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
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

        public async Task SendRequestAsync<TResponse>(Request<TResponse> request)
            where TResponse : Response
        {
            EnsureIsInitialized();

        }

        public async Task<SyncResults> SynchronizeAsync(CancellationToken cancellationToken)
        {
            EnsureIsInitialized();
            await _syncLock.WaitAsync(cancellationToken);

            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Interlocked.Exchange(ref _syncLock, null)?.Dispose();
                Interlocked.Exchange(ref _syncDbContext, null)?.Dispose();
            }
        }

        protected void EnsureIsInitialized()
        {
            if (!_isInitializeCompleted)
                throw new InvalidOperationException($"The {nameof(SyncEngine)} has not been initialized.");
        }
    }
}

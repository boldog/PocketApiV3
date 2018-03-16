using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace PocketApiV3.Persistence.Data
{
    public class ModelSynchronization
    {
        readonly LiteRepository _repository;

        enum Result
        {
            Equal,
            Created,
            Updated,
        }

        public ModelSynchronization(LiteRepository repository)
        {
            _repository = repository;
        }

        public Models.PocketItem CreateOrUpdateDatabaseModel(RetrieveResponseItem retrieveResponseItem)
        {
            if (retrieveResponseItem == null)
                throw new ArgumentNullException(nameof(retrieveResponseItem));

            var now = DateTimeOffset.UtcNow;

            var dbItem = _repository.SingleOrDefault<Models.PocketItem>(Query.EQ(nameof(Models.PocketItem.Id), retrieveResponseItem.ItemId));
            if (dbItem == null)
                dbItem = new Models.PocketItem() { TimeSyncDatabaseAdded = now };


            dbItem.AmpUrl = retrieveResponseItem.AmpUrl;

            //dbItem.Authors
            var apiAuthors = (IList<IPocketAuthorData>)retrieveResponseItem.Authors?.Select(x => x.Value) ?? Array.Empty<IPocketAuthorData>();
            //if (AreEquatableListsEqual<IPocketAuthorData>(apiAuthors, dbItem.Authors, PocketAuthorDataOperations.Equals) == false)
            //{
            //    dbItem.Authors.Clear();
            //    dbItem.Authors.AddRange(apiAuthors.Select(x => new Models.PocketAuthor(x)));
            //}

            //dbItem.Encoding
            dbItem.Excerpt = retrieveResponseItem.Excerpt;
            dbItem.GivenTitle = retrieveResponseItem.GivenTitle;
            dbItem.GivenUrl = retrieveResponseItem.GivenUrl;
            dbItem.Id = retrieveResponseItem.ItemId;
            dbItem.ImageContent = retrieveResponseItem.ImageContent;

            //dbItem.Images


            dbItem.IsArticle = retrieveResponseItem.IsArticle;
            dbItem.IsFavorite = retrieveResponseItem.IsFavorite;
            dbItem.IsIndex = retrieveResponseItem.IsIndex;
            //dbItem.LeadImage
            //dbItem.MimeType =
            dbItem.ResolvedId = retrieveResponseItem.ResolvedId;
            dbItem.ResolvedTitle = retrieveResponseItem.ResolvedTitle;
            dbItem.ResolvedUrl = retrieveResponseItem.ResolvedUrl;
            dbItem.Status = retrieveResponseItem.Status;
            //dbItem.Tags
            dbItem.TimeAdded = retrieveResponseItem.TimeAdded;
            dbItem.TimeSyncDatabaseUpdated = now;
            dbItem.TimeFavorited = retrieveResponseItem.TimeFavorited;
            dbItem.TimeRead = retrieveResponseItem.TimeRead;
            dbItem.TimeUpdated = retrieveResponseItem.TimeUpdated;
            dbItem.VideoContent = retrieveResponseItem.VideoContent;
            //dbItem.Videos
            dbItem.WordCount = retrieveResponseItem.WordCount;

            _repository.Upsert(dbItem);

            return dbItem;
        }

        //bool CopyToDatabaseModel(PocketApiV3.RetrieveResponseItem apiModel, Models.PocketItem dbModel)
        //{
        //    if (apiModel == null)
        //        throw new ArgumentNullException(nameof(apiModel));
        //    if (dbModel.Id != 0 && dbModel.Id != apiModel.ItemId)
        //        throw new ArgumentException($"The passed {nameof(PocketApiV3.RetrieveResponseItem)} ID \"{apiModel.ItemId}\" differs from this {nameof(Models.PocketItem)} ID \"{dbModel.Id}\".");

        //    bool changed = false;
        //    bool effectiveBool;
        //    DateTime? effectiveDateTime;

        //    if (dbModel.Id != apiModel.ItemId)
        //    {
        //        dbModel.Id = apiModel.ItemId;
        //        changed = true;
        //    }

        //    if (dbModel.ResolvedId != apiModel.ResolvedId)
        //    {
        //        dbModel.ResolvedId = apiModel.ResolvedId;
        //        changed = true;
        //    }

        //    if (dbModel.GivenUrl != apiModel.GivenUrl)
        //    {
        //        dbModel.GivenUrl = apiModel.GivenUrl;
        //        changed = true;
        //    }

        //    if (dbModel.GivenTitle != apiModel.GivenTitle)
        //    {
        //        dbModel.GivenTitle = apiModel.GivenTitle;
        //        changed = true;
        //    }

        //    effectiveBool = apiModel.IsFavorite ?? false;
        //    if (dbModel.IsFavorite != effectiveBool)
        //    {
        //        dbModel.IsFavorite = effectiveBool;
        //        changed = true;
        //    }

        //    if (dbModel.Status != apiModel.Status)
        //    {
        //        dbModel.Status = apiModel.Status;
        //        changed = true;
        //    }

        //    effectiveDateTime = apiModel.TimeAdded?.DateTime;
        //    if (dbModel.TimeAdded != effectiveDateTime)
        //    {
        //        dbModel.TimeAdded = effectiveDateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = apiModel.TimeUpdated?.DateTime;
        //    if (dbModel.TimeUpdated != effectiveDateTime)
        //    {
        //        dbModel.TimeUpdated = effectiveDateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = apiModel.TimeRead?.DateTime;
        //    if (dbModel.TimeRead != effectiveDateTime)
        //    {
        //        dbModel.TimeRead = apiModel.TimeRead?.DateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = apiModel.TimeFavorited?.DateTime;
        //    if (dbModel.TimeFavorited != effectiveDateTime)
        //    {
        //        dbModel.TimeFavorited = effectiveDateTime;
        //        changed = true;
        //    }

        //    if (dbModel.ResolvedTitle != apiModel.ResolvedTitle)
        //    {
        //        dbModel.ResolvedTitle = apiModel.ResolvedTitle;
        //        changed = true;
        //    }

        //    if (dbModel.ResolvedUrl != apiModel.ResolvedUrl)
        //    {
        //        dbModel.ResolvedUrl = apiModel.ResolvedUrl;
        //        changed = true;
        //    }

        //    if (dbModel.Excerpt != apiModel.Excerpt)
        //    {
        //        dbModel.Excerpt = apiModel.Excerpt;
        //        changed = true;
        //    }

        //    if (dbModel.IsArticle != apiModel.IsArticle)
        //    {
        //        dbModel.IsArticle = apiModel.IsArticle;
        //        changed = true;
        //    }

        //    if (dbModel.IsIndex != apiModel.IsIndex)
        //    {
        //        dbModel.IsIndex = apiModel.IsIndex;
        //        changed = true;
        //    }

        //    if (dbModel.ImageContent != apiModel.ImageContent)
        //    {
        //        dbModel.ImageContent = apiModel.ImageContent;
        //        changed = true;
        //    }

        //    if (dbModel.VideoContent != apiModel.VideoContent)
        //    {
        //        dbModel.VideoContent = apiModel.VideoContent;
        //        changed = true;
        //    }

        //    if (dbModel.WordCount != apiModel.WordCount)
        //    {
        //        dbModel.WordCount = apiModel.WordCount;
        //        changed = true;
        //    }

        //    if (dbModel.AmpUrl != apiModel.AmpUrl)
        //    {
        //        dbModel.AmpUrl = apiModel.AmpUrl;
        //        changed = true;
        //    }

        //    var apiModelAuthors = (IList<PocketAuthor>)apiModel.Authors?.Select(x => x.Value).ToList()
        //        ?? Array.Empty<PocketAuthor>();

        //    bool apiModelHasAuthors = apiModelAuthors.Count > 0;
        //    bool dbModelHasAuthors = dbModel.Authors != null && dbModel.Authors.Count > 0;

        //    if (apiModelHasAuthors && dbModelHasAuthors)
        //    {
        //        changed |= CopyToDatabaseModel(apiModelAuthors, dbModel.Authors);
        //    }
        //    else if (apiModelHasAuthors && !dbModelHasAuthors)
        //    {
        //        changed |= CopyToDatabaseModel(apiModelAuthors, dbModel.Authors = new List<Models.PocketAuthor>());
        //    }
        //    else if (!apiModelHasAuthors && dbModelHasAuthors)
        //    {
        //        changed |= CopyToDatabaseModel(Array.Empty<PocketApiV3.PocketAuthor>(), dbModel.Authors);
        //    }



        //    //var dbAuthor = _repository.SingleOrDefault<Models.PocketAuthor>(Query.EQ(nameof(Models.PocketAuthor.Id),))




        //    //dbModel.LeadImage = PocketImage.CreateCopyFrom(apiModel.Image);

        //    // TODO - sync PocketItem collections members

        //    //SyncCollection(Authors, retrieveResponseItem.Authors);
        //    //SyncCollection(Images, retrieveResponseItem.Images);

        //    //LeadImageId = retrieveResponseItem.Image?.Id;

        //    //// TODO - sync tags
        //    //SyncCollection(Videos, retrieveResponseItem.Videos);

        //    return changed;
        //}

        //bool CopyToDatabaseModel(IList<PocketApiV3.PocketAuthor> apiAuthorList, List<Models.PocketAuthor> dbAuthorList)
        //{
        //    bool changed = false;

        //    if (dbAuthorList.Capacity < apiAuthorList.Count)
        //        dbAuthorList.Capacity = apiAuthorList.Count;

        //    for (int index = 0; index < apiAuthorList.Count; index++)
        //    {
        //        var apiAuthor = apiAuthorList[index];
        //        int indexOfExisting = dbAuthorList.FindIndex(x => x.Id == apiAuthor.Id);
        //        if (indexOfExisting == -1)
        //        {
        //            // Lookup up or create DB author
        //            dbAuthorList[index] = GetDbAuthor(apiAuthor);
        //            changed = true;
        //        }
        //        else if (indexOfExisting != index)
        //        {
        //            var existing = dbAuthorList[indexOfExisting];
        //            dbAuthorList.RemoveAt(indexOfExisting);
        //            dbAuthorList.Insert(index, existing);
        //            changed = true;
        //        }
        //    }

        //    // Remove excess items
        //    dbAuthorList.RemoveRange(apiAuthorList.Count, dbAuthorList.Count - apiAuthorList.Count);

        //    return changed;
        //}

        bool CopyToDatabaseModel(IList<PocketApiV3.PocketImage> apiImageList, List<Models.PocketImage> dbImageList)
        {
            bool changed = false;

            if (dbImageList.Capacity < apiImageList.Count)
                dbImageList.Capacity = apiImageList.Count;

            for (int index = 0; index < apiImageList.Count; index++)
            {
                var apiObject = apiImageList[index];
                int indexOfExisting = dbImageList.FindIndex(x => x.Equals(apiObject));
            }

            throw new NotImplementedException();
        }

        //Models.PocketAuthor GetDbAuthor(PocketApiV3.PocketAuthor apiAuthor)
        //{
        //    var dbAuthor = _repository.SingleOrDefault<Models.PocketAuthor>(Query.EQ(nameof(Models.PocketAuthor.Id), apiAuthor.Id));
        //    if (dbAuthor == null)
        //    {
        //        dbAuthor = new Models.PocketAuthor()
        //        {
        //            Id = apiAuthor.Id,
        //            Name = apiAuthor.Name,
        //            Url = apiAuthor.Url
        //        };
        //        _repository.Insert(dbAuthor);
        //        return dbAuthor;
        //    }
        //    else
        //    {
        //        bool changed = false;
        //        if (dbAuthor.Name != apiAuthor.Name)
        //        {
        //            dbAuthor.Name = apiAuthor.Name;
        //            changed = true;
        //        }
        //        if (dbAuthor.Url != apiAuthor.Url)
        //        {
        //            dbAuthor.Url = apiAuthor.Url;
        //            changed = true;
        //        }

        //        if (changed)
        //            _repository.Update(dbAuthor);

        //        return dbAuthor;
        //    }
        //}

        //bool CopyToDatabaseModel(PocketApiV3.PocketImage apiModel, Models.PocketImage dbModel)
        //{
        //    throw new NotImplementedException();

        //}



        static bool AreEquatableListsEqual<T>(List<T> x, IList<T> y, Func<T, T, bool> equalityFunc)
        {
            if (x.Count != y.Count)
                return false;

            for (int index = 0; index < x.Count; index++)
            {
                if (equalityFunc(x[index], y[index]) == false)
                    return false;
            }

            return true;
        }
    }
}

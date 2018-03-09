using System;
using System.Collections.Generic;
using System.Linq;
using PocketApiV3.Persistence.Extensions;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketItem
    {
        public long Id { get; set; }

        public long ResolvedId { get; set; }

        public string GivenUrl { get; set; }

        public string GivenTitle { get; set; }
        public bool IsFavorite { get; set; }
        public PocketItemStatus Status { get; set; }
        public DateTimeOffset? TimeAdded { get; set; }
        public DateTimeOffset? TimeUpdated { get; set; }
        public DateTimeOffset? TimeRead { get; set; }
        public DateTimeOffset? TimeFavorited { get; set; }
        public DateTimeOffset TimeAddedToSyncDatabase { get; set; }
        public string ResolvedTitle { get; set; }
        public string ResolvedUrl { get; set; }
        public string Excerpt { get; set; }
        public bool? IsArticle { get; set; }
        public bool? IsIndex { get; set; }
        public SubsumptionRelationship? ImageContent { get; set; }
        public SubsumptionRelationship? VideoContent { get; set; }
        public int? WordCount { get; set; }
        public string AmpUrl { get; set; }


        public string Encoding { get; set; }
        public string MimeType { get; set; }



        // --------------------------------------------------------------------
        // Complete Properties
        // --------------------------------------------------------------------

        public ICollection<PocketAuthor> Authors { get; set; } = new List<PocketAuthor>();

        public long? LeadImageId { get; set; }

        public ICollection<PocketImage> Images { get; set; } = new List<PocketImage>();

        public ICollection<PocketItemTag> Tags { get; set; } = new List<PocketItemTag>();

        public ICollection<PocketVideo> Videos { get; set; } = new List<PocketVideo>();

        public void CopyFrom(PocketApiV3.RetrieveResponseItem retrieveResponseItem)
        {
            if (retrieveResponseItem == null)
                throw new ArgumentNullException(nameof(retrieveResponseItem));
            if (Id != 0 && Id != retrieveResponseItem.ItemId)
                throw new ArgumentException($"The passed {nameof(PocketApiV3.RetrieveResponseItem)} ID \"{retrieveResponseItem.ItemId}\" differs from this {nameof(PocketItem)} ID \"{Id}\".");
            if (retrieveResponseItem.ItemId == null)
                throw new ArgumentException($"The passed {nameof(PocketApiV3.RetrieveResponseItem)} ID is null.");

            Id = retrieveResponseItem.ItemId.Value;
            ResolvedId = retrieveResponseItem.ResolvedId ?? 0;

            GivenUrl = retrieveResponseItem.GivenUrl;
            GivenTitle = retrieveResponseItem.GivenTitle;
            IsFavorite = retrieveResponseItem.IsFavorite ?? false;
            Status = retrieveResponseItem.Status;
            TimeAdded = retrieveResponseItem.TimeAdded;
            TimeUpdated = retrieveResponseItem.TimeUpdated;
            TimeRead = retrieveResponseItem.TimeRead;
            TimeFavorited = retrieveResponseItem.TimeFavorited;
            ResolvedTitle = retrieveResponseItem.ResolvedTitle;
            ResolvedUrl = retrieveResponseItem.ResolvedUrl;
            Excerpt = retrieveResponseItem.Excerpt;
            IsArticle = retrieveResponseItem.IsArticle;
            IsIndex = retrieveResponseItem.IsIndex;
            ImageContent = retrieveResponseItem.ImageContent;
            VideoContent = retrieveResponseItem.VideoContent;
            WordCount = retrieveResponseItem.WordCount;
            AmpUrl = retrieveResponseItem.AmpUrl;

            SyncCollection(Authors, retrieveResponseItem.Authors);
            SyncCollection(Images, retrieveResponseItem.Images);
            LeadImageId = retrieveResponseItem.Image?.Id;

            // TODO - sync tags
            SyncCollection(Videos, retrieveResponseItem.Videos);
        }

        static void SyncCollection<TPersistenceType, TApiType>(ICollection<TPersistenceType> persistenceItemCollection,
            Dictionary<long, TApiType> apiItemDictionary)
            where TPersistenceType : ICanCopyFrom<TApiType>, IIdentifiable<long>, new()
        {
            if (apiItemDictionary == null)
                return;

            if (persistenceItemCollection.Count > 0)
            {
                // Check for authors we need to remove.
                var idsToRemove =
                    persistenceItemCollection
                    .Select(x => x.Id)
                    .Except(apiItemDictionary.Select(x => x.Key))
                    .ToHashSet();

                if (idsToRemove.Count > 0)
                {
                    var itemsToRemove = persistenceItemCollection.Where(x => idsToRemove.Contains(x.Id)).ToList();
                    foreach (var x in itemsToRemove)
                        persistenceItemCollection.Remove(x);
                }
            }

            foreach (var apiItem in apiItemDictionary)
            {
                var persistenceItem = persistenceItemCollection.SingleOrDefault(x => x.Id == apiItem.Key);
                if (persistenceItem == null)
                {
                    persistenceItem = new TPersistenceType();
                    persistenceItem.CopyFrom(apiItem.Value);
                    persistenceItemCollection.Add(persistenceItem);
                }
                else
                {
                    persistenceItem.CopyFrom(apiItem.Value);
                }
            }
        }

    }
}

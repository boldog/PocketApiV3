using System;
using System.Collections.Generic;
using System.Linq;
using PocketApiV3.Persistence.Extensions;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketItem
        : IEquatable<PocketItem>
    {
        public PocketItem()
        {
            Authors = new List<PocketAuthor>();
            Images = new List<PocketImage>();
            Tags = new List<string>();
            Videos = new List<PocketVideo>();
        }

        public long Id { get; set; }

        public long ResolvedId { get; set; }

        public string GivenUrl { get; set; }

        public string GivenTitle { get; set; }
        public bool? IsFavorite { get; set; }
        public PocketItemStatus Status { get; set; }
        public DateTimeOffset? TimeAdded { get; set; }
        public DateTimeOffset? TimeUpdated { get; set; }
        public DateTimeOffset? TimeRead { get; set; }
        public DateTimeOffset? TimeFavorited { get; set; }
        public DateTimeOffset TimeSyncDatabaseAdded { get; set; }
        public DateTimeOffset TimeSyncDatabaseUpdated { get; set; }
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

        public List<PocketAuthor> Authors { get; set; }

        public PocketImage LeadImage { get; set; }

        public List<PocketImage> Images { get; set; }

        public List<string> Tags { get; set; }

        public List<PocketVideo> Videos { get; set; }

        //public bool CopyFrom(PocketApiV3.RetrieveResponseItem other)
        //{
        //    if (other == null)
        //        throw new ArgumentNullException(nameof(other));
        //    if (Id != 0 && Id != other.ItemId)
        //        throw new ArgumentException($"The passed {nameof(PocketApiV3.RetrieveResponseItem)} ID \"{other.ItemId}\" differs from this {nameof(PocketItem)} ID \"{Id}\".");
        //    if (other.ItemId == null)
        //        throw new ArgumentException($"The passed {nameof(PocketApiV3.RetrieveResponseItem)} ID is null.");

        //    bool changed = false;
        //    long effectiveLong;
        //    bool effectiveBool;
        //    DateTime? effectiveDateTime;

        //    effectiveLong = other.ItemId ?? 0;
        //    if (Id != effectiveLong)
        //    {
        //        Id = effectiveLong;
        //        changed = true;
        //    }

        //    effectiveLong = other.ResolvedId ?? 0;
        //    if (ResolvedId != effectiveLong)
        //    {
        //        ResolvedId = effectiveLong;
        //        changed = true;
        //    }

        //    if (GivenUrl != other.GivenUrl)
        //    {
        //        GivenUrl = other.GivenUrl;
        //        changed = true;
        //    }

        //    if (GivenTitle != other.GivenTitle)
        //    {
        //        GivenTitle = other.GivenTitle;
        //        changed = true;
        //    }

        //    effectiveBool = other.IsFavorite ?? false;
        //    if (IsFavorite != effectiveBool)
        //    {
        //        IsFavorite = effectiveBool;
        //        changed = true;
        //    }

        //    if (Status != other.Status)
        //    {
        //        Status = other.Status;
        //        changed = true;
        //    }

        //    effectiveDateTime = other.TimeAdded?.DateTime;
        //    if (TimeAdded != effectiveDateTime)
        //    {
        //        TimeAdded = effectiveDateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = other.TimeUpdated?.DateTime;
        //    if (TimeUpdated != effectiveDateTime)
        //    {
        //        TimeUpdated = effectiveDateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = other.TimeRead?.DateTime;
        //    if (TimeRead != effectiveDateTime)
        //    {
        //        TimeRead = other.TimeRead?.DateTime;
        //        changed = true;
        //    }

        //    effectiveDateTime = other.TimeFavorited?.DateTime;
        //    if (TimeFavorited != effectiveDateTime)
        //    {
        //        TimeFavorited = effectiveDateTime;
        //        changed = true;
        //    }

        //    if (ResolvedTitle != other.ResolvedTitle)
        //    {
        //        ResolvedTitle = other.ResolvedTitle;
        //        changed = true;
        //    }

        //    if (ResolvedUrl != other.ResolvedUrl)
        //    {
        //        ResolvedUrl = other.ResolvedUrl;
        //        changed = true;
        //    }

        //    if (Excerpt != other.Excerpt)
        //    {
        //        Excerpt = other.Excerpt;
        //        changed = true;
        //    }

        //    if (IsArticle != other.IsArticle)
        //    {
        //        IsArticle = other.IsArticle;
        //        changed = true;
        //    }

        //    if (IsIndex != other.IsIndex)
        //    {
        //        IsIndex = other.IsIndex;
        //        changed = true;
        //    }

        //    if (ImageContent != other.ImageContent)
        //    {
        //        ImageContent = other.ImageContent;
        //        changed = true;
        //    }

        //    if (VideoContent != other.VideoContent)
        //    {
        //        VideoContent = other.VideoContent;
        //        changed = true;
        //    }

        //    if (WordCount != other.WordCount)
        //    {
        //        WordCount = other.WordCount;
        //        changed = true;
        //    }

        //    if (AmpUrl != other.AmpUrl)
        //    {
        //        AmpUrl = other.AmpUrl;
        //        changed = true;
        //    }

        //    LeadImage = PocketImage.CreateCopyFrom(other.Image);

        //    // TODO - sync PocketItem collections members

        //    //SyncCollection(Authors, retrieveResponseItem.Authors);
        //    //SyncCollection(Images, retrieveResponseItem.Images);

        //    //LeadImageId = retrieveResponseItem.Image?.Id;

        //    //// TODO - sync tags
        //    //SyncCollection(Videos, retrieveResponseItem.Videos);

        //    return changed;
        //}

        public override bool Equals(object obj) =>
            obj is PocketItem other ? Equals(other) : base.Equals(obj);

        public bool Equals(PocketItem other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id.Equals(other.Id)
                && ResolvedId.Equals(other.ResolvedId)
                && Equals(GivenUrl, other.GivenUrl)
                && Equals(GivenTitle, other.GivenTitle)
                && IsFavorite.Equals(other.IsFavorite)
                && Status.Equals(other.Status)
                && TimeAdded.Equals(other.TimeAdded)
                && TimeUpdated.Equals(other.TimeUpdated)
                && TimeRead.Equals(other.TimeRead)
                && TimeFavorited.Equals(other.TimeFavorited)
                && TimeSyncDatabaseAdded.Equals(other.TimeSyncDatabaseAdded)
                && TimeSyncDatabaseUpdated.Equals(other.TimeSyncDatabaseUpdated)
                && Equals(ResolvedTitle, other.ResolvedTitle)
                && Equals(ResolvedUrl, other.ResolvedUrl)
                && Equals(Excerpt, other.Excerpt)
                && IsArticle.Equals(other.IsArticle)
                && IsIndex.Equals(other.IsIndex)
                && ImageContent.Equals(other.ImageContent)
                && VideoContent.Equals(other.VideoContent)
                && WordCount.Equals(other.WordCount)
                && Equals(AmpUrl, other.AmpUrl)
                && Equals(Encoding, other.Encoding)
                && Equals(MimeType, other.MimeType)
                && Equals(LeadImage, other.LeadImage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 47;
                hashCode = (hashCode * 53) ^ Id.GetHashCode();
                hashCode = (hashCode * 53) ^ ResolvedId.GetHashCode();
                if (GivenUrl != null)
                    hashCode = (hashCode * 53) ^ GivenUrl.GetHashCode();
                if (GivenTitle != null)
                    hashCode = (hashCode * 53) ^ GivenTitle.GetHashCode();
                hashCode = (hashCode * 53) ^ IsFavorite.GetHashCode();
                hashCode = (hashCode * 53) ^ (int)Status;
                hashCode = (hashCode * 53) ^ TimeAdded.GetHashCode();
                hashCode = (hashCode * 53) ^ TimeUpdated.GetHashCode();
                hashCode = (hashCode * 53) ^ TimeRead.GetHashCode();
                hashCode = (hashCode * 53) ^ TimeFavorited.GetHashCode();
                hashCode = (hashCode * 53) ^ TimeSyncDatabaseAdded.GetHashCode();
                hashCode = (hashCode * 53) ^ TimeSyncDatabaseUpdated.GetHashCode();
                if (ResolvedTitle != null)
                    hashCode = (hashCode * 53) ^ ResolvedTitle.GetHashCode();
                if (ResolvedUrl != null)
                    hashCode = (hashCode * 53) ^ ResolvedUrl.GetHashCode();
                if (Excerpt != null)
                    hashCode = (hashCode * 53) ^ Excerpt.GetHashCode();
                hashCode = (hashCode * 53) ^ IsArticle.GetHashCode();
                hashCode = (hashCode * 53) ^ IsIndex.GetHashCode();
                hashCode = (hashCode * 53) ^ ImageContent.GetHashCode();
                hashCode = (hashCode * 53) ^ VideoContent.GetHashCode();
                hashCode = (hashCode * 53) ^ WordCount.GetHashCode();
                if (AmpUrl != null)
                    hashCode = (hashCode * 53) ^ AmpUrl.GetHashCode();
                if (Encoding != null)
                    hashCode = (hashCode * 53) ^ Encoding.GetHashCode();
                if (MimeType != null)
                    hashCode = (hashCode * 53) ^ MimeType.GetHashCode();
                if (LeadImage != null)
                    hashCode = (hashCode * 53) ^ LeadImage.GetHashCode();
                return hashCode;
            }
        }

        //static void SyncCollection<TPersistenceType, TApiType>(ICollection<TPersistenceType> persistenceItemCollection,
        //    Dictionary<long, TApiType> apiItemDictionary)
        //    where TPersistenceType : ICanCopyFrom<TApiType>, new()
        //{
        //    if (apiItemDictionary == null)
        //        return;

        //    if (persistenceItemCollection.Count > 0)
        //    {
        //        // Check for authors we need to remove.
        //        var idsToRemove =
        //            persistenceItemCollection
        //            .Select(x => x.Id)
        //            .Except(apiItemDictionary.Select(x => x.Key))
        //            .ToHashSet();

        //        if (idsToRemove.Count > 0)
        //        {
        //            var itemsToRemove = persistenceItemCollection.Where(x => idsToRemove.Contains(x.Id)).ToList();
        //            foreach (var x in itemsToRemove)
        //                persistenceItemCollection.Remove(x);
        //        }
        //    }

        //    foreach (var apiItem in apiItemDictionary)
        //    {
        //        var persistenceItem = persistenceItemCollection.SingleOrDefault(x => x.Id == apiItem.Key);
        //        if (persistenceItem == null)
        //        {
        //            persistenceItem = new TPersistenceType();
        //            persistenceItem.CopyFrom(apiItem.Value);
        //            persistenceItemCollection.Add(persistenceItem);
        //        }
        //        else
        //        {
        //            persistenceItem.CopyFrom(apiItem.Value);
        //        }
        //    }
        //}

        public static bool operator ==(PocketItem x, PocketItem y) =>
            Common.Equals(x, y);
        public static bool operator !=(PocketItem x, PocketItem y) =>
            !Common.Equals(x, y);
    }
}

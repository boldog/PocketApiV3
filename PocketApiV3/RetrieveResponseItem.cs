using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RetrieveResponseItem
    {
        /// <summary>
        /// A unique identifier matching the saved item. This id must be used
        /// to perform any actions through the v3/modify endpoint.
        /// </summary>
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        /// <summary>
        /// A unique identifier similar to the item_id but is unique to the
        /// actual url of the saved item. The resolved_id identifies unique
        /// urls. For example a direct link to a New York Times article and a
        /// link that redirects (ex a shortened bit.ly url) to the same article
        /// will share the same resolved_id. If this value is 0, it means that
        /// Pocket has not processed the item. Normally this happens within
        /// seconds but is possible you may request the item before it has been
        /// resolved.
        /// </summary>
        [JsonProperty("resolved_id")]
        public long? ResolvedId { get; set; }

        /// <summary>
        /// The actual url that was saved with the item. This url should be used
        /// if the user wants to view the item.
        /// </summary>
        [JsonProperty("given_url")]
        public string GivenUrl { get; set; }

        /// <summary>
        /// The title that was saved along with the item.
        /// </summary>
        [JsonProperty("given_title")]
        public string GivenTitle { get; set; }

        /// <summary>
        /// True if the item is favorited.
        /// </summary>
        [JsonProperty("favorite")]
        public bool? IsFavorite { get; set; }

        [JsonProperty("status")]
        public PocketItemStatus Status { get; set; }

        [JsonProperty("time_added")]
        public DateTimeOffset? TimeAdded { get; set; }

        [JsonProperty("time_updated")]
        public DateTimeOffset? TimeUpdated { get; set; }

        [JsonProperty("time_read")]
        public DateTimeOffset? TimeRead { get; set; }

        [JsonProperty("time_favorited")]
        public DateTimeOffset? TimeFavorited { get; set; }

        [JsonProperty("sort_id")]
        public int SortIndex { get; set; }

        /// <summary>
        /// The title that Pocket found for the item when it was parsed.
        /// </summary>
        [JsonProperty("resolved_title")]
        public string ResolvedTitle { get; set; }

        /// <summary>
        /// The final url of the item. For example if the item was a shortened
        /// bit.ly link, this will be the actual article the url linked to.
        /// </summary>
        [JsonProperty("resolved_url")]
        public string ResolvedUrl { get; set; }

        /// <summary>
        /// The first few lines of the item (articles only)
        /// </summary>
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        /// <summary>
        /// If the parser thinks this item is an article it will be set to
        /// <see langword="true"/>.
        /// </summary>
        [JsonProperty("is_article")]
        public bool? IsArticle { get; set; }

        /// <summary>
        /// If the parser thinks this item is an index page it will be set to
        /// <see langword="true"/>.
        /// </summary>
        [JsonProperty("is_index")]
        public bool? IsIndex { get; set; }

        [JsonProperty("has_image")]
        public SubsumptionRelationship? ImageContent { get; set; }

        [JsonProperty("has_video")]
        public SubsumptionRelationship? VideoContent { get; set; }

        [JsonProperty("word_count")]
        public int? WordCount { get; set; }

        [JsonProperty("amp_url")]
        public string AmpUrl { get; set; }


        // --------------------------------------------------------------------
        // Complete Properties
        // --------------------------------------------------------------------

        [JsonProperty("authors")]
        public Dictionary<long, PocketAuthor> Authors { get; set; }

        [JsonProperty("image")]
        public PocketImage Image { get; set; }

        [JsonProperty("images")]
        public Dictionary<long, PocketImage> Images { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, PocketTag> Tags { get; set; }

        [JsonProperty("videos")]
        public Dictionary<long, PocketVideo> Videos { get; set; }

    }
}

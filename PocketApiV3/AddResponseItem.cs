using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class AddResponseItem
    {
        /// <summary>
        /// A unique identifier for the added item
        /// </summary>
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        /// <summary>
        /// The original url for the added item
        /// </summary>
        [JsonProperty("normal_url")]
        public string NormalUrl { get; set; }

        /// <summary>
        /// A unique identifier for the resolved item
        /// </summary>
        [JsonProperty("resolved_id")]
        public long? ResolvedId { get; set; }

        [JsonProperty("extended_item_id")]
        public long? ExtendedItemId { get; set; }

        /// <summary>
        /// The resolved url for the added item. The easiest way to think about
        /// the resolved_url - if you add a bit.ly link, the resolved_url will 
        /// be the url of the page the bit.ly link points to..
        /// </summary>
        [JsonProperty("resolved_url")]
        public string ResolvedUrl { get; set; }

        /// <summary>
        /// A unique identifier for the domain of the resolved_url.
        /// </summary>
        [JsonProperty("domain_id")]
        public long? DomainId { get; set; }

        /// <summary>
        /// A unique identifier for the domain of the normal_url.
        /// </summary>
        [JsonProperty("origin_domain_id")]
        public long? OriginDomainId { get; set; }

        /// <summary>
        /// The response code received by the Pocket parser when it tried to access the item.
        /// </summary>
        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        /// <summary>
        /// The MIME type returned by the item.
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        /// <summary>
        /// The content length of the item.
        /// </summary>
        [JsonProperty("content_length")]
        public string ContentLength { get; set; }

        /// <summary>
        /// The encoding of the item.
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// The date the item was resolved.
        /// </summary>
        [JsonProperty("date_resolved")]
        //public System.DateTimeOffset DateResolved { get; set; }
        public string DateResolved { get; set; }

        /// <summary>
        /// The date the item was published (if the parser was able to find one).
        /// </summary>
        [JsonProperty("date_published")]
        //public System.DateTimeOffset DatePublished { get; set; }
        public string DatePublished { get; set; }

        /// <summary>
        /// The title of the resolved_url.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The excerpt of the resolved_url.
        /// </summary>
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        /// <summary>
        /// For an article, the number of words.
        /// </summary>
        [JsonProperty("word_count")]
        public string WordCount { get; set; }

        [JsonProperty("innerdomain_redirect")]
        public string InnerdomainRedirect { get; set; }

        [JsonProperty("login_required")]
        public string LoginRequired { get; set; }

        [JsonProperty("has_image")]
        public SubsumptionRelationship? HasImage { get; set; }

        [JsonProperty("has_video")]
        public SubsumptionRelationship? HasVideo { get; set; }

        /// <summary>
        /// If the parser thinks this item is an index page it will be set to
        /// true.
        /// </summary>
        [JsonProperty("is_index")]
        public bool? IsIndex { get; set; }

        /// <summary>
        /// If the parser thinks this item is an article it will be set to true.
        /// </summary>
        [JsonProperty("is_article")]
        public bool? IsArticle { get; set; }

        [JsonProperty("used_fallback")]
        public string UsedFallback { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("time_first_parsed")]
        public string TimeFirstParsed { get; set; }

        /// <summary>
        /// Array of author data(if author(s) were found)
        /// </summary>
        [JsonProperty("authors")]
        public Dictionary<long, PocketAuthor> Authors { get; set; }

        /// <summary>
        /// Array of image data (if image(s) were found)
        /// </summary>
        [JsonProperty("images")]
        public Dictionary<long, PocketImage> Images { get; set; }

        /// <summary>
        /// Array of video data (if video(s) were found)
        /// </summary>
        [JsonProperty("videos")]
        public Dictionary<long, PocketVideo> Videos { get; set; }

        [JsonProperty("top_image_url")]
        public string TopImageUrl { get; set; }

        [JsonProperty("resolved_normal_url")]
        public string ResolvedNormalUrl { get; set; }

        [JsonProperty("given_url")]
        public string GivenUrl { get; set; }


    }
}

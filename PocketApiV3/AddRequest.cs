using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class AddRequest : Request<AddResponse>
    {
        internal override bool ApiAuthRequired => true;
        internal override string ApiMethod => "add";
        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.FormUrlEncoded;

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("tweet_id")]
        public long? TweetId { get; set; }

        [JsonIgnore]
        public string TagsDelimited
        {
            get => TagHelper.Join(Tags);
            set => Tags = TagHelper.Split(value);
        }
    }
}

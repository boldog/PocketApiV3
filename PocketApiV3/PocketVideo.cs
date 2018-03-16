using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PocketVideo : IPocketVideoData
    {
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        [JsonProperty("video_id")]
        public long? Id { get; set; }

        [JsonProperty("src")]
        public string Url { get; set; }

        public int? Height
        {
            get => int.TryParse(_height, out int x) ? x : default(int?);
            set => _height = value?.ToString();
        }

        public int? Width
        {
            get => int.TryParse(_width, out int x) ? x : default(int?);
            set => _width = value?.ToString();
        }

        public PocketVideoType Type
        {
            get
            {
                switch (TypeRaw ?? "")
                {
                    case "1": return PocketVideoType.YouTube;
                    case "2":
                    case "3":
                    case "4":
                        return PocketVideoType.Vimeo;
                    case "5":
                        return PocketVideoType.Html;
                    case "6":
                        return PocketVideoType.Flash;
                    default:
                        return PocketVideoType.Undefined;
                }
            }
        }

        [JsonProperty("_type")]
        public string TypeRaw { get; set; }

        [JsonProperty("vid")]
        public string ExternalId { get; set; }

        [JsonProperty("height")]
        string _height;

        [JsonProperty("width")]
        string _width;
    }
}

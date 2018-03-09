using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PocketImage
    {
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        [JsonProperty("image_id")]
        public long? Id { get; set; }

        [JsonProperty("src")]
        public string Url { get; set; }

        [JsonProperty("credit")]
        public string Credit { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

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



        [JsonProperty("height")]
        string _height;

        [JsonProperty("width")]
        string _width;

    }
}

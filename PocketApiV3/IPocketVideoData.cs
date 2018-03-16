using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public interface IPocketVideoData
    {
        string Url { get; set; }
        int? Height { get; set; }
        int? Width { get; set; }
        string TypeRaw { get; set; }
        string ExternalId { get; set; }
    }
}

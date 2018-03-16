using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public interface IPocketImageData
    {
        string Url { get; set; }
        string Credit { get; set; }
        string Caption { get; set; }
        int? Height { get; set; }
        int? Width { get; set; }
    }
}

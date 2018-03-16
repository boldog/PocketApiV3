using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3
{
    public interface IPocketAuthorData
    {
        string Name { get; set; }

        string Url { get; set; }
    }
}

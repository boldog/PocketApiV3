using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence.Data.Models
{
    public interface IIdentifiable<TIdentifier> where TIdentifier : struct
    {
        TIdentifier Id { get; }
    }
}

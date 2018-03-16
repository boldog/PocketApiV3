using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public interface ICanCopyFrom<T>
    {

        bool CopyFrom(T other);
    }
}

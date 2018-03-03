using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public abstract class Request<TResponse> : Request
        where TResponse : Response
    {
        protected Request() { }
    }

}

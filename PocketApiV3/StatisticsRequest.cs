using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public sealed class StatisticsRequest : Request<StatisticsResponse>
    {
        internal override bool ApiAuthRequired => true;

        internal override string ApiMethod => "stats";

        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.JsonBody;
    }
}

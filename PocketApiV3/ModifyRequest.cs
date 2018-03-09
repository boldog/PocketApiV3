using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class ModifyRequest : Request<ModifyResponse>
    {
        public ModifyRequest()
        {
            Actions = new List<ModifyActions.Action>();
        }

        public ModifyRequest(ModifyActions.Action action)
            : this()
        {
            Actions.Add(action);
        }

        public ModifyRequest(IEnumerable<ModifyActions.Action> actions)
            : this()
        {
            Actions.AddRange(actions);
        }

        [JsonProperty("actions")]
        public List<ModifyActions.Action> Actions { get; }


        internal override bool ApiAuthRequired => true;
        internal override string ApiMethod => "send";
        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.FormUrlEncoded;

    }
}

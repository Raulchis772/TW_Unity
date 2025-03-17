using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace TW_EventSub_Models
{

    public class TW_EventSubRequestBody
    {
        public static JObject CreateRequestBody(string type, string version, string sessionID)
        {
            JObject requestBody = new JObject();
            requestBody["type"] = type;
            requestBody["version"] = version;
            JObject transport = new JObject();
            transport["method"] = "websocket";
            transport["session_id"] = sessionID;
            requestBody["transport"] = transport;
            return requestBody;
        }
    }
}

using Newtonsoft.Json;
using UnityEngine;

namespace TW_EventSub_Models
{
    public struct TW_Subscription
    {
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("version")]
        public string Version;
        [JsonProperty("created_at")]
        public string CreatedAt;
    }
}

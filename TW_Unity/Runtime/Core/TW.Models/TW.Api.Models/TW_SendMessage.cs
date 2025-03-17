using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_SendMessage
    {
        [JsonProperty("data")]
        public TW_ResponseSendMessage[] response;
    }

    public class TW_ResponseSendMessage
    {
        [JsonProperty("message_id")]
        public string messageID;
        [JsonProperty("is_sent")]
        public bool isSent;
        [JsonProperty("drop_reason")]
        public TW_DropReason dropReason;
    }
    public class TW_DropReason
    {
        [JsonProperty("code")]
        public string code;
        [JsonProperty("message")]
        public string message;
    }
}

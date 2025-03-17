using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public struct TW_Models_WebSocket
    {
        [JsonProperty("metadata")]
        TW_Metadata Metadata;
        [JsonProperty("payload")]
        TW_Payload Payload;
    }

    public struct TW_Metadata
    {
        [JsonProperty("message_id")]
        public string MessageID;
        [JsonProperty("message_type")]
        public string MessageType;
        [JsonProperty("message_timestamp")]
        public string MessageTimeStamp;
    }

    public struct TW_Payload
    {
        [JsonProperty("session")]
        public TW_Session Session;
    }

    public struct TW_Session
    {
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("keepalive_timeout_seconds")]
        public int KeepAliveTimeoutSeconds;
        [JsonProperty("reconnect_url")]
        public string ReconnectURL;
        [JsonProperty("connected_at")]
        public string ConnectedAt;
    }
}


using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models {
    public class TW_ChannelRaid
    {
        [JsonProperty("from_broadcaster_user_id")]
        public string FromBroadcasterUserId;
        [JsonProperty("from_broadcaster_user_login")]
        public string FromBroadcasterUserLogin;
        [JsonProperty("from_broadcaster_user_name")]
        public string FromBroadcasterUserName;
        [JsonProperty("to_broadcaster_user_id")]
        public string ToBroadcasterUserId;
        [JsonProperty("to_broadcaster_user_login")]
        public string ToBroadcasterUserLogin;
        [JsonProperty("to_broadcaster_user_name")]
        public string ToBroadcasterUserName;
        [JsonProperty("viewers")]
        public int Viewers;
    }
}

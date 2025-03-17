using Newtonsoft.Json;

namespace TW_Models
{
    public class TW_ChannelSubscriptionMessage
    {
        [JsonProperty("user_id")]
        public string userID;
        [JsonProperty("user_login")]
        public string userLogin;
        [JsonProperty("user_name")]
        public string userName;
        [JsonProperty("broadcaster_user_id")]
        public string broadcasterUserID;
        [JsonProperty("broadcaster_user_login")]
        public string broadcasterUserLogin;
        [JsonProperty("broadcaster_user_name")]
        public string broadcasterUserName;
        [JsonProperty("tier")]
        public string tier;
        [JsonProperty("message")]
        public TW_SubscriptionMessage message; 
    }

    public class TW_SubscriptionMessage
    {
        [JsonProperty("text")]
        public string text;
        [JsonProperty("emotes")]
        public TW_EmoteSuscription[] emotes;
    }
}

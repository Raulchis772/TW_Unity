using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelCheer
    {
        [JsonProperty("is_anonymous")]
        public bool isAnonymous;
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
        [JsonProperty("message")]
        public string message;
        [JsonProperty("bits")]
        public int bits;

    }

}

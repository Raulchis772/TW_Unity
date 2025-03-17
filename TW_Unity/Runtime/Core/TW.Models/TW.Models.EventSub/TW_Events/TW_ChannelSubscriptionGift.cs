using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelSubscriptionGift
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
        [JsonProperty("total")]
        public int total;
        [JsonProperty("tier")]
        public string tier;
        [JsonProperty("cumulative_total")]
        public int cumulativeTotal;
        [JsonProperty("is_anunymous")]
        public bool isAnonymous;
    }

}

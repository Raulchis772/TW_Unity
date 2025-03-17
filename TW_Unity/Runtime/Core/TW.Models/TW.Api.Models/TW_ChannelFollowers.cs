using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelFollowers
    {
        [JsonProperty("data")]
        public TW_ChannelFollowersData[] data;
        [JsonProperty("pagination")]
        public TW_Pagination pagination;
        [JsonProperty("total")]
        public int total;
    }

    public class TW_ChannelFollowersData
    {
        [JsonProperty("followed_at")]
        public string followedAt;
        [JsonProperty("user_id")]
        public string userID;
        [JsonProperty("user_login")]
        public string userLogin;
        [JsonProperty("user_name")]
        public string userName;
    }
}
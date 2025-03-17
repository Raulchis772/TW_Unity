using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChattersResponse
    {
        [JsonProperty("data")]
        public TW_ChattersData[] data;
        [JsonProperty("pagination")]
        public TW_Pagination pagination;
        [JsonProperty("total")]
        public int total;
    }

    public class TW_ChattersData
    {
        [JsonProperty("user_id")]
        public string userId;
        [JsonProperty("user_login")]
        public string userLogin;
        [JsonProperty("user_name")]
        public string userName;

    }
}

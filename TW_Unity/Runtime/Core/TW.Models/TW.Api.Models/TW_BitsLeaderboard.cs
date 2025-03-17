using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_BitsLeaderboard
    {
        [JsonProperty("data")]
        public TW_Leaders[] leaderBoard;
        [JsonProperty("date_range")]
        public TW_DateRange dateRange;
        [JsonProperty("total")]
        public int total;
    }

    public class TW_Leaders
    {
        [JsonProperty("user_id")]
        public string userID;
        [JsonProperty("user_login")]
        public string userLogin;
        [JsonProperty("user_name")]
        public string userName;
        [JsonProperty("rank")]
        public int rank;
        [JsonProperty("score")]
        public int score;

    }

    public class TW_DateRange
    {
        [JsonProperty("started_at")]
        public string startedAt;
        [JsonProperty("ended_at")]
        public string endedAt;
    }
}


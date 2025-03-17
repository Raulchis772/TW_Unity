using Newtonsoft.Json;

namespace TW_Models
{
    public class TW_CustomRewardRedemption
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("broadcaster_user_id")]
        public string broadcasterUserID;
        [JsonProperty("broadcaster_user_login")]
        public string broadcasterUserLogin;
        [JsonProperty("broadcaster_user_name")]
        public string broadcasterUserName;
        [JsonProperty("user_id")]
        public string userID;
        [JsonProperty("user_login")]
        public string userLogin;
        [JsonProperty("user_name")]
        public string userName;
        [JsonProperty("user_input")]
        public string userInput;
        [JsonProperty("status")]
        public string status;
        [JsonProperty("reward")]
        public TW_CustomReward reward;
        [JsonProperty("redeemed_at")]
        public string redeemedAt;
    }
}


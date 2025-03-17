using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelAutomaticRewardRedemption
    {
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
        [JsonProperty("id")]
        public string id;
        [JsonProperty("reward")]
        public TW_AutomaticReward reward;
        [JsonProperty("message")] 
        public TW_Message message;
        [JsonProperty("redeemed_at")]
        public string redeemedAt;
    }

    public class TW_AutomaticReward
    {
        [JsonProperty("type")]
        public string type;
        [JsonProperty("channel_points")]
        public int channelpoints;
        [JsonProperty("emote")]
        public TW_AutomaticRewardEmote unlockedEmote;
    }

    public class TW_AutomaticRewardEmote
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("name")]
        public string name;
    }
}

using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelChatMessage 
    {
        [JsonProperty("broadcaster_user_id")]
        public string broadcasterUserID;
        [JsonProperty("broadcaster_user_name")]
        public string broadcasterUserName;
        [JsonProperty("broadcaster_user_login")]
        public string broadcasterUserLogin;
        [JsonProperty("chatter_user_id")]
        public string chatterUserID;
        [JsonProperty("chatter_user_name")]
        public string chatterUserName;
        [JsonProperty("chatter_user_login")]
        public string chatterUserLogin;
        [JsonProperty("message_id")]
        public string messageID;
        [JsonProperty("message")]
        public TW_Message message;
        [JsonProperty("message_type")]
        public string messageType;
        [JsonProperty("badges")]
        public TW_Badge[] badges;
        [JsonProperty("cheer")]
        public TW_Cheer cheer;
        [JsonProperty("reply")]
        public TW_Reply reply;
        [JsonProperty("channel_points_custom_reward_id")]
        public string channelPointsCustomRewardID;
        [JsonProperty("source_broadcaster_user_id")]
        public string sourceBroadcasterUserID;
        [JsonProperty("source_broadcaster_user_name")]
        public string sourceBroadcasterUserName;
        [JsonProperty("source_broadcaster_user_login")]
        public string sourceBroadcasterUserLogin;
        [JsonProperty("source_message_id")]
        public string sourceMessageID;
        [JsonProperty("source_badges")]
        public TW_Badge[] sourceBadges;
    }
}

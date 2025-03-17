using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_ChannelInformation
    {
        [JsonProperty("data")]
        public TW_ChannelInformationData[] data;
    }

    public class TW_ChannelInformationData
    {
        [JsonProperty("broadcaster_id")]
        public string broadcasterID;
        [JsonProperty("broadcaster_login")]
        public string broadcasterLogin;
        [JsonProperty("broadcaster_name")]
        public string broadcasterName;
        [JsonProperty("broadcaster_language")]
        public string broadcasterLanguage;
        [JsonProperty("game_name")]
        public string gameName;
        [JsonProperty("game_id")]
        public string gameID;
        [JsonProperty("title")]
        public string title;
        [JsonProperty("delay")]
        public int delay;
        [JsonProperty("tags")]
        public string[] tags;
        [JsonProperty("content_classification_labels")]
        public string[] contentClassificationLabels;
        [JsonProperty("is_branded_content")]
        public bool isBrandedContent;
    }
}

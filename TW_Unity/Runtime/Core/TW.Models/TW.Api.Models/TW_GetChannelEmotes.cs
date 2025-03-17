using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{ 
    public class TW_GetChannelEmotesResponse
    {
        [JsonProperty("data")]
        public TW_GetChannelEmotesData[] data;
        [JsonProperty("template")]
        public string template;
    }

    public class TW_GetChannelEmotesData
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("name")]
        public string name;
        [JsonProperty("images")]
        public TW_Models_Images images;
        [JsonProperty("tier")]
        public string tier;
        [JsonProperty("emote_type")]
        public string emoteType;
        [JsonProperty("emote_set_id")]
        public string emoteId;
        [JsonProperty("format")]
        public string[] format;
        [JsonProperty("scale")]
        public string[] scale;
        [JsonProperty("theme_mode")]
        public string[] themeMode;
    }
}

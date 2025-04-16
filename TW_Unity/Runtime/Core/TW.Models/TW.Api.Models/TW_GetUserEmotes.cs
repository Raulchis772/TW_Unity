using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_GetUserEmotesResponse
    {
        [JsonProperty("data")]
        public TW_GetUserEmotesData[] data;
        [JsonProperty("template")]
        public string template;
        [JsonProperty("pagination")]
        public TW_Pagination pagination;
    }

    public class TW_GetUserEmotesData
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("name")]
        public string name;
        [JsonProperty("emote_type")]
        public string emoteType;
        [JsonProperty("emote_set_id")]
        public string emoteId;
        [JsonProperty("owner_id")]
        public string ownerId;
        [JsonProperty("format")]
        public string[] format;
        [JsonProperty("scale")]
        public string[] scale;
        [JsonProperty("theme_mode")]
        public string[] themeMode;
    }
}

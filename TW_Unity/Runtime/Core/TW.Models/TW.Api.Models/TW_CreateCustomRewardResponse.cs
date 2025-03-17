using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_CreateCustomRewardResponse
    {
        [JsonProperty("data")]
        public TW_Reward[] Reward;
    }

    public class TW_NewCustomReward
    {
        [JsonProperty("title")]
        public string title;
        [JsonProperty("cost")]
        public int cost;
        [JsonProperty("prompt")]
        public string prompt;
        [JsonProperty("is_enabled")]
        public bool isEneabled;
        [JsonProperty("background_color")]
        public string backgroundColor;
        [JsonProperty("is_user_input_required")]
        public bool isUserInputRequired;
        [JsonProperty("is_max_per_stream_enabled")]
        public bool isMaxPerStreamEnabled;
        [JsonProperty("max_per_stream")]
        public int maxPerStream;
        [JsonProperty("is_max_per_user_per_stream_enabled")]
        public bool isMaxPerUserPerStreamEnabled;
        [JsonProperty("max_per_user_per_stream")]
        public int maxPerUserPerStream;
        [JsonProperty("is_global_cooldown_enabled")]
        public bool isGlobalCooldownEnabled;
        [JsonProperty("global_cooldown_seconds")]
        public int globalCooldownSeconds;
        [JsonProperty("should_redemptions_skip_request_queue")]
        public bool shouldRedemptionsSkipRequestQueue;
    }
}


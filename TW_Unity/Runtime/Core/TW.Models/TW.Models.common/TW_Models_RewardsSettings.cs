using Newtonsoft.Json;
using System;
using UnityEngine;

namespace TW_Models
{
    public class TW_Models_RewardPerStreamSettings
    {
        [JsonProperty("is_enabled")]
        public bool IsEneabled;
        [JsonProperty("max_per_stream")]
        public int MaxPerStream;
    }

    public class TW_Models_RewardPerUserPerStreamSettings
    {
        [JsonProperty("is_enabled")]
        public bool IsEneabled;
        [JsonProperty("max_per_user_per_stream")]
        public int MaxPerUserPerStream;
    }

    public class TW_Models_GlobalCooldownSettings {
        [JsonProperty("is_enabled")]
        public bool IsEneabled;
        [JsonProperty("global_cooldown_seconds")]
        public Int64 GlobalCooldownSeconds;
    }

    public class TW_NewReward
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


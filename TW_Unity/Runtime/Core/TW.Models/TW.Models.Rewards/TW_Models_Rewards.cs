
using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_Reward
    {
        [JsonProperty("broadcaster_id")]
        public string BroadCasterID;
        [JsonProperty("broadcaster_login")]
        public string BroadCasterLogin;
        [JsonProperty("broadcaster_name")]
        public string BroadCasterName;
        [JsonProperty("id")]
        public string RewardID;
        [JsonProperty("title")]
        public string RewardTitle;
        [JsonProperty("prompt")]
        public string RewardPrompt;
        [JsonProperty("cost")]
        public string Cost;
        [JsonProperty("image")]
        public TW_Models_Images Image;
        [JsonProperty("default_image")]
        public TW_Models_Images DefaultImage;
        [JsonProperty("background_color")]
        public string BackgroundColor;
        [JsonProperty("is_enabled")]
        public bool IsEneabled;
        [JsonProperty("is_user_input_required")]
        public bool UserInputRequired;
        [JsonProperty("max_per_stream_setting")]
        public TW_Models_RewardPerStreamSettings RewardPerStreamSettings;
        [JsonProperty("max_per_user_per_stream_setting")]
        public TW_Models_RewardPerUserPerStreamSettings RewardPerUserPerStreamSettings;
        [JsonProperty("global_cooldown_setting")]
        public TW_Models_GlobalCooldownSettings GlobalCooldownSettings;
        [JsonProperty("is_paused")]
        public bool IsPaused;
        [JsonProperty("is_in_stock")]
        public bool IsInStock;
        [JsonProperty("should_redemptions_skip_request_queue")]
        public bool RedemptionSkipQueue;
        [JsonProperty("redemptions_redeemed_current_stream")]
        public int? RedemtionsCurrentStream;
        [JsonProperty("cooldown_expires_at")]
        public string CooldownExpiresAt;
    }
    
    public class  TW_CustomReward
    {
        [JsonProperty("id")]
        public string RewardID;
        [JsonProperty("title")]
        public string RewardTitle;
        [JsonProperty("prompt")]
        public string RewardPrompt;
        [JsonProperty("cost")]
        public string Cost;
    }
}


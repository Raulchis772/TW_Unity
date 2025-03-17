using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public struct TW_CustomRewards
    {
        [JsonProperty("data")]
        public TW_Reward[] Reward;


    }

   
}

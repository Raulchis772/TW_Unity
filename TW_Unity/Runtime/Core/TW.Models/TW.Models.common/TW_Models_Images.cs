using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_Models_Images
    {
        [JsonProperty("url_1x")]
        public string image1x;
        [JsonProperty("url_2x")]
        public string image2x;
        [JsonProperty("url_4x")]
        public string image4x;
    }

}

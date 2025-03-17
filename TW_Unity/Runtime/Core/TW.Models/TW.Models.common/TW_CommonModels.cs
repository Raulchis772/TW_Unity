using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_Pagination
    {
        [JsonProperty("cursor")]
        public string cursor;
    }
}

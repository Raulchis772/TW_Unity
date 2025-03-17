using UnityEngine;

namespace TW_WebHelper
{
    public class TW_WebResponse
    {
        public string data;
        public string error;
        public bool IsSuccess => string.IsNullOrEmpty(error);
    }

}

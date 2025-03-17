using System.Collections.Generic;
using UnityEngine;

namespace TW_WebHelper  
{
    public class TW_Headers
    {
        public Dictionary<string, string> headers { get; private set; }
    }

    public enum RequestType
    {
        GET, POST, PATCH, DELETE
    }

   public enum RequestError
    {
        OK, POSTDATAMISSING, REQUESTERROR
    }
}


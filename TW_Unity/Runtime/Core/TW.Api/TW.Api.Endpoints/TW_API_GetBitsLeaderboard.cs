using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TW_Models;
using TW_WebHelper;
using UnityEngine;

namespace TW_API_Functions
{
    public class TW_API_GetBitsLeaderboard : MonoBehaviour
    {
        public static async UniTask<TW_BitsLeaderboard> GetBitsLeadboard(int count = 0, string period = null, string startedAt = null, string userID = null)
        {
            TW_AuthDataHandler authDataHandler = FindFirstObjectByType<TW_AuthDataHandler>();

            if (!TW_APIHelpers.AuthDataHandlerHasData(authDataHandler))
            {
                return default;
            }

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authDataHandler.authData.clientID);
            headers.Add("Authorization", "Bearer " + authDataHandler.authData.authToken);

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (count > 0)
            {
                query.Add("count", count.ToString());
            }

            if (!string.IsNullOrEmpty(period))
            {
                query.Add("period", period);
            }
            if (!string.IsNullOrEmpty(startedAt))
            {
                query.Add("started_at", startedAt);

            }
            if (!string.IsNullOrEmpty(userID))
            {
                query.Add("user_id", userID);
            }

            TW_WebResponse data = await TW_WebRequest.CommonRequest("bits/leaderboard", queryParameters: query, headers: headers);

            if (data.IsSuccess)
            {
                JObject obj = JObject.Parse(data.data);
                return obj.ToObject<TW_BitsLeaderboard>();
            }
            else
            {
                Debug.LogError("request Error: " + data.error);
                return default;
            }
        }
    }
}


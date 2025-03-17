using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TW_API_Functions;
using TW_Models;
using TW_WebHelper;
using UnityEngine;

namespace TW_API_Functions
{
    public class TW_API_GetChannelFollowers : MonoBehaviour
    {
        public static async UniTask<TW_ChannelFollowers> GetChannelFollowers(string broadcasterID = null, string userID = null, int first = 0, string after = null)
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
            if (!string.IsNullOrEmpty(broadcasterID))
            {
                query.Add("broadcaster_id", broadcasterID);
            }
            else
            {
                query.Add("broadcaster_id", authDataHandler.authData.authenticatedAccountID);
            }

            if (!string.IsNullOrEmpty(userID))
            {
                query.Add("user_id", userID);
            }

            if (!string.IsNullOrEmpty(after))
            {
                query.Add("after", after);
            }
            if (first != 0)
            {
                query.Add("first", first.ToString());
            }
            TW_WebResponse data = await TW_WebRequest.CommonRequest("channels/followers", queryParameters: query, headers: headers);

            if (data.IsSuccess)
            {
                JObject obj = JObject.Parse(data.data);



                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                TW_ChannelFollowers responseFollowers = JsonConvert.DeserializeObject<TW_ChannelFollowers>(data.data);
                return responseFollowers;

            }
            else
            {
                Debug.LogError("request Error: " + data.error);
                return default;

            }
        }
    }
}
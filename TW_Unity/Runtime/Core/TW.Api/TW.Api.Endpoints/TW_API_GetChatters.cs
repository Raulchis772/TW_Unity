using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TW_Models;
using TW_WebHelper;
using UnityEngine;
namespace TW_API_Functions
{


    public class TW_API_GetChatters : MonoBehaviour
    {
        public static async UniTask<TW_ChattersResponse> GetChatters(string broadcasterID = null, string first = null, string after = null)
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

            query.Add("moderator_id", authDataHandler.authData.authenticatedAccountID);

            if (!string.IsNullOrEmpty(first))
            {
                query.Add("first", first);
            }

            if (!string.IsNullOrEmpty(after))
            {
                query.Add("after", after);
            }

            TW_WebResponse data = await TW_WebRequest.CommonRequest("chat/chatters", queryParameters: query, headers: headers);
            if (data.IsSuccess)
            {
                JObject obj = JObject.Parse(data.data);
                return obj.ToObject<TW_ChattersResponse>();
            }
            else
            {
                Debug.LogError("request Error: " + data.error);
                return null;
            }
        }

    }
}


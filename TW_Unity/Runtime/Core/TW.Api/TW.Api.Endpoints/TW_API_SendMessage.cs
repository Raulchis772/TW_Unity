using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TW_Models;
using TW_WebHelper;
using UnityEngine;

namespace TW_API_Functions
{
    public class TW_API_SendMessage: MonoBehaviour
    {
        public static async UniTask<TW_SendMessage> SendMessage(string message, string broadcasterID=null, string replyParentMessageID = null)
        {

        
            TW_AuthDataHandler authDataHandler = FindFirstObjectByType<TW_AuthDataHandler>();

            if (!TW_APIHelpers.AuthDataHandlerHasData(authDataHandler))
            {
                return default;
            }

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authDataHandler.authData.clientID);
            headers.Add("Authorization", "Bearer " + authDataHandler.authData.authToken);
            headers.Add("Content-Type", "application/json");

            JObject body = new JObject();
            body.Add("message", message);
            if (!string.IsNullOrEmpty(broadcasterID))
            {
                body.Add("broadcaster_id", broadcasterID);
            }
            else
            {
                body.Add("broadcaster_id", authDataHandler.authData.authenticatedAccountID);
            }

            if (!string.IsNullOrEmpty(replyParentMessageID))
            {
                body.Add("reply_parent_message_id", replyParentMessageID);
            }

            body.Add("sender_id", authDataHandler.authData.authenticatedAccountID);

            TW_WebResponse response = await TW_WebRequest.CommonRequest("chat/messages", RequestType.POST, headers: headers, body: body.ToString());

            if (response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<TW_SendMessage>(response.data);
            }
            else
            {
                Debug.LogError("Error sending message: " + response);
                return null;
            }
           
           
        }
    }
}


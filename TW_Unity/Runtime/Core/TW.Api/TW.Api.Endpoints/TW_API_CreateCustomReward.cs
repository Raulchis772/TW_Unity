using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TW_Models;
using TW_WebHelper;
using UnityEngine;

namespace TW_API_Functions
{
    public class TW_API_CreateCustomReward : MonoBehaviour
    {
        public static async UniTask<TW_CreateCustomRewardResponse> CreateCustomReward(string tittle, int cost)
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

            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("broadcaster_id", authDataHandler.authData.authenticatedAccountID);

            JObject body = new JObject();
            body.Add("title", tittle);
            body.Add("cost", cost);


            TW_WebResponse response = await TW_WebRequest.CommonRequest("channel_points/custom_rewards", RequestType.POST, queryParameters: queryParameters, headers: headers, body: body.ToString());

            if (response.IsSuccess)
            {
                TW_CreateCustomRewardResponse reward = JsonConvert.DeserializeObject<TW_CreateCustomRewardResponse>(response.data);
                 return reward;
             
            }
            else
            {
                Debug.LogError("Error creating custom reward: " + response.error);
                return default;

            }
        }


        public static async UniTask<TW_CreateCustomRewardResponse> CreateCustomReward(TW_NewCustomReward NewCustomReward)
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

            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("broadcaster_id", authDataHandler.authData.authenticatedAccountID);
            
            string body = JsonConvert.SerializeObject(NewCustomReward);


            TW_WebResponse response = await TW_WebRequest.CommonRequest("channel_points/custom_rewards", RequestType.POST, queryParameters: queryParameters, headers: headers, body: body.ToString());

            if (response.IsSuccess)
            {
                TW_CreateCustomRewardResponse reward = JsonConvert.DeserializeObject<TW_CreateCustomRewardResponse>(response.data);
                return reward;

            }
            else
            {
                Debug.LogError("Error creating custom reward: " + response.error);
                return default;

            }
        }
    }

}
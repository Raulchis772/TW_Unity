using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TW_Models;
using TW_WebHelper;
using UnityEditor;
using UnityEngine;


namespace TW_API_Functions {
   
    
public class TW_API_GetCustomReward : MonoBehaviour
    {
    
        public static async UniTask<TW_CustomRewards> GetCustomReward(string rewardID = "", bool onlyManageableRewards = false)
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
            query.Add("broadcaster_id", authDataHandler.authData.authenticatedAccountID);

            if (!string.IsNullOrEmpty(rewardID))
            {
                query.Add("id", rewardID);
            }
            if (onlyManageableRewards)
            {
                query.Add("only_manageable_rewards", "true");
            }
            //TW_Reward responseReward = new TW_Reward();
            
            TW_WebResponse data  = await TW_WebRequest.CommonRequest("channel_points/custom_rewards", queryParameters: query, headers: headers);


            if (data.IsSuccess)
            {
                JObject obj = JObject.Parse(data.data);
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                TW_CustomRewards responseReward = obj.ToObject<TW_CustomRewards>();
                return responseReward;
            }
            else
            {
                Debug.LogError("request Error: " + data.error);
                return default;
            }
           
            
           
        }
    }
}


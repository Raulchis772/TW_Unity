using Cysharp.Threading.Tasks;
using NativeWebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TW_EventSub_Models;
using TW_Models;
using TW_WebHelper;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace TW_EventSub
{
    public class TW_EventSubHandler
    {
        private TW_AuthData authData;
        WebSocket currentWebSocket;
        string eventID;
        bool authTokenIsOK = true;
        #region delegates
        public delegate void OnMessageReceived(TW_ChannelChatMessage message);
        public delegate void OnCustomRewardRedemption(TW_CustomRewardRedemption message);
        public delegate void OnFollow(TW_Follow message);
        public delegate void OnChannelSubscription(TW_ChannelSubscribe message);
        public delegate void OnChannelSubscriptionGift(TW_ChannelSubscriptionGift message);
        public delegate void OnChannelResubscription(TW_ChannelSubscriptionMessage message);
        public delegate void OnChannelCheer(TW_ChannelCheer message);
        public delegate void OnChannelRaid(TW_ChannelRaid message);
        public delegate void OnAutomaticRewardRedemption(TW_ChannelAutomaticRewardRedemption message);
        public delegate void OnEventSubError(JObject error);
        public delegate void OnEventSubConnect();
        public delegate void OnEventSubDisconnect();
        #endregion

        #region events
        public event OnMessageReceived onMessageReceived;
        public event OnCustomRewardRedemption onCustomRewardRedemption;
        public event OnFollow onFollow;
        public event OnChannelSubscription onChannelSubscription;
        public event OnChannelSubscriptionGift onChannelSubscriptionGift;
        public event OnChannelResubscription onChannelResubscription;
        public event OnChannelCheer onChannelCheer;
        public event OnChannelRaid onChannelRaid;
        public event OnAutomaticRewardRedemption onAutomaticRewardRedemption;
        public event OnEventSubError onEventSubError;
        public event OnEventSubConnect onEventSubConnect;
        public event OnEventSubDisconnect onEventSubDisconnect;
        #endregion

        #region constructors

        public TW_EventSubHandler(TW_AuthDataHandler AuthDataHandler)
        {
            if (!AuthDataHandler.authData.AuthTokenHasData())
            {
                Debug.LogError("AuthData in AuthDataHandler is empty");
                return;
            }
            if (!AuthDataHandler.authData.ClientIDHasData())
            {
                Debug.LogError("ClientID in AuthDataHandler is empty");
                return;
            }
            if (!AuthDataHandler.authData.BroadcasterIDHasData())
            {
                Debug.LogError("BroadcasterID in AuthDataHandler is empty");
                return;
            }

            authData = AuthDataHandler.authData;
            //StartEventListener().Forget();
        }

        public void StartConnection(TW_AuthDataHandler AuthDataHandler)
        {
            if (!AuthDataHandler.authData.AuthTokenHasData())
            {
                Debug.LogError("AuthData in AuthDataHandler is empty");
                return;
            }
            if (!AuthDataHandler.authData.ClientIDHasData())
            {
                Debug.LogError("ClientID in AuthDataHandler is empty");
                return;
            }
            if (!AuthDataHandler.authData.BroadcasterIDHasData())
            {
                Debug.LogError("BroadcasterID in AuthDataHandler is empty");
                return;
            }

            authData = AuthDataHandler.authData;


            StartEventListener().Forget();
        }

        #endregion


        public async UniTask StartEventListener()
        {
            authTokenIsOK = true;
            Debug.Log("startEventListenerActive");
            string uri = "wss://eventsub.wss.twitch.tv/ws";

            WebSocket webSocket = new WebSocket(uri);

            webSocket.OnOpen += () =>
            {
                onEventSubConnect?.Invoke();
            };

            webSocket.OnError += (e) =>
            {
                JObject error = new JObject { "error", e.ToString() };
                onEventSubError?.Invoke(JObject.Parse(e.ToString()));
            };

            webSocket.OnClose += (e) =>
            {
                onEventSubDisconnect?.Invoke();
            };

            webSocket.OnMessage += ProcessMessage;

            currentWebSocket = webSocket;

            DispatchQueue().Forget();
            await webSocket.Connect();
            Debug.Log("WebSocket connected");

        }


        async UniTaskVoid DispatchQueue()
        {
            float time = 0;
            while (true)
            {

                currentWebSocket.DispatchMessageQueue();
                await UniTask.Yield();
                time += Time.deltaTime;
                if (time > 5)
                {
                    if (currentWebSocket.State == WebSocketState.Closed)
                    {
                        Debug.Log("WebSocket closed");
                        break;
                    }
                }
            }
        }


        void ProcessMessage(byte[] message)
        {

            JObject obj = JObject.Parse(System.Text.Encoding.UTF8.GetString(message));
            switch ((string)obj.SelectToken("metadata.message_type"))
            {
                case "session_welcome":

                    eventID = (string)obj.SelectToken("payload.session.id");
                    break;
                case "session_keepalive":
                    break;

                case "session_reconnect":
                    Debug.LogWarning("reconnect  websocket");
                    break;

                default:
                    TW_EventSubDispatcher.AnalizeEventSubMessage(obj, this);
                    break;
            }


        }

        void DebugMessage(string message)
        {
            Debug.Log(message);
        }
        async UniTask CheckWebSocket()
        {

            if (string.IsNullOrEmpty(eventID))
            {
                if (currentWebSocket == null || (authTokenIsOK && currentWebSocket.State == WebSocketState.Closed))
                {
                    StartEventListener().Forget();

                    while (string.IsNullOrEmpty(eventID))
                    {
                        Debug.Log("waiting EventID");
                        await UniTask.Yield();
                    }
                }
            }
        }

        public void SubscriptionError(string e)
        {
            if (authTokenIsOK)
            {
                int indexOfObjectErrorStart = e.ToString().IndexOf("{");
                int indexOfObjectErrorEnd = e.ToString().IndexOf("}");
                JObject error = JObject.Parse(e.ToString().Substring(indexOfObjectErrorStart, indexOfObjectErrorEnd - indexOfObjectErrorStart + 1));
                switch (error["error"].ToString())
                {
                    case "Unauthorized":
                        if (error["message"].ToString() == "Invalid OAuth token")
                        {
                            currentWebSocket.Close();
                            authTokenIsOK = false;
                            Debug.Log("patatatatatataa");
                        }
                        Debug.LogError(error["message"]);
                        break;
                    default:
                        Debug.LogError("Failed to subscribe the eventsub event" + error.ToString());
                        break;
                }

                onEventSubError?.Invoke(error);
            }
        }

        #region subscribtionsEvents
        public async UniTaskVoid StartSubscribeChannelChatMessage()
        {
            await CheckWebSocket();

            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }

            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.chat.message", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID},
                {"user_id", authData.authenticatedAccountID  }
            };

            requestBody["condition"] = condition;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");

            Debug.Log("hola desde chatmessage" + authTokenIsOK);

            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());

            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeCustomRewardRedemption(string rewardID = null)
        {
            await CheckWebSocket();

            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.channel_points_custom_reward_redemption.add", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID},
                };

            if (!string.IsNullOrEmpty(rewardID))
            {
                condition["reward_id"] = rewardID;
            }

            requestBody["condition"] = condition;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");

            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeFollow()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.follow", "2", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID},
                {"moderator_user_id", authData.authenticatedAccountID    }
                };

            requestBody["condition"] = condition;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");


            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeChannelSubscription()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.subscribe", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID}
                };

            requestBody["condition"] = condition;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");


            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeChannelSubscriptionGift()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.subscription.gift", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID}
                };

            requestBody["condition"] = condition;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");

            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());

            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());

            }
        }

        public async UniTaskVoid StartSubscribeChannelResubscription()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.subscription.message", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID}
                };

            requestBody["condition"] = condition;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");


            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeChannelCheer()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }

            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.cheer", "1", eventID);

            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID}
                };

            requestBody["condition"] = condition;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");


            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeRaidToBroadcaster()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.raid", "1", eventID);

            JObject condition = new JObject {
                {"to_broadcaster_user_id",  authData.authenticatedAccountID}
                };

            requestBody["condition"] = condition;


            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");

            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e) { 
                SubscriptionError(e.ToString());
            }
        }

        public async UniTaskVoid StartSubscribeAutomaticRewardRedemption()
        {
            await CheckWebSocket();
            if (string.IsNullOrEmpty(eventID))
            {
                bool existSessionID = await WaitForSessionID();
                if (!existSessionID)
                {
                    return;
                }
            }
            JObject requestBody = TW_EventSubRequestBody.CreateRequestBody("channel.channel_points_automatic_reward_redemption.add", "2", eventID);
            JObject condition = new JObject {
                {"broadcaster_user_id",  authData.authenticatedAccountID}
                };
            requestBody["condition"] = condition;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Client-Id", authData.clientID);
            headers.Add("Authorization", "Bearer " + authData.authToken);
            headers.Add("Content-Type", "application/json");

            Debug.Log("hola desde automatic reward" + currentWebSocket.State);
            

            try
            {
                TW_WebResponse response = await TW_WebRequest.CommonRequest("eventsub/subscriptions", requestType: RequestType.POST, headers: headers, body: requestBody.ToString());
            }
            catch (Exception e)
            {
                SubscriptionError(e.ToString());
            }

        }

        #endregion


        #region CallSubscriptions
        public void SubscribeChannelChatMessage()
        {
            StartSubscribeChannelChatMessage().Forget();
        }

        public void SubscribeCustomRewardRedemption(string rewardID = null)
        {
            StartSubscribeCustomRewardRedemption(rewardID).Forget();
        }

        public void SubscribeFollow()
        {
            StartSubscribeFollow().Forget();
        }

        public void SubscribeChannelSubscription()
        {
            StartSubscribeChannelSubscription().Forget();
        }

        public void SubscribeChannelSubscriptionGift()
        {
            StartSubscribeChannelSubscriptionGift().Forget();
        }

        public void SubscribeChannelResubscription()
        {
            StartSubscribeChannelResubscription().Forget();
        }

        public void SubscribeChannelCheer()
        {
            StartSubscribeChannelCheer().Forget();
        }

        public void SubscribeRaidToBroadcaster()
        {
            StartSubscribeRaidToBroadcaster().Forget();
        }

        public void SubscribeAutomaticRewardRedemption()
        {
            StartSubscribeAutomaticRewardRedemption().Forget();
        }
        #endregion
        public async UniTask<bool> WaitForSessionID()
        {
            float waitTime = 0;
            while (string.IsNullOrEmpty(eventID))
            {

                await UniTask.Yield();
                waitTime += Time.deltaTime;
                if (waitTime > 5)
                {
                    Debug.LogError("EventID not found");
                    return false;
                }
            }
            return true;
        }

        #region invokeEvents
        public void OnMessageReceivedInvoke(TW_ChannelChatMessage message)
        {
            onMessageReceived?.Invoke(message);
        }

        public void OnCustomRewardRedemptionInvoke(TW_CustomRewardRedemption message)
        {
            onCustomRewardRedemption?.Invoke(message);
        }

        public void OnFollowInvoke(TW_Follow message)
        {
            onFollow?.Invoke(message);
        }

        public void OnChannelSubscriptionInvoke(TW_ChannelSubscribe message)
        {
            onChannelSubscription?.Invoke(message);
        }

        public void OnChannelSubscriptionGiftInvoke(TW_ChannelSubscriptionGift message)
        {
            onChannelSubscriptionGift?.Invoke(message);
        }

        public void OnChannelResubscriptionInvoke(TW_ChannelSubscriptionMessage message)
        {
            onChannelResubscription?.Invoke(message);
        }

        public void OnChannelCheerInvoke(TW_ChannelCheer message)
        {
            onChannelCheer?.Invoke(message);
        }

        public void OnChannelRaidInvoke(TW_ChannelRaid message)
        {
            onChannelRaid?.Invoke(message);
        }

        public void OnAutomaticRewardRedemptionInvoke(TW_ChannelAutomaticRewardRedemption message)
        {
            onAutomaticRewardRedemption?.Invoke(message);
        }
        #endregion


        public async UniTask CloseConnection()
        {
            if (currentWebSocket != null)
            {
                await currentWebSocket.Close();
            }
        }

    }
}

#region testURI
public class testURI
{
    public static string uri = "ws://127.0.0.1:8080/ws";
    public static string uriSubscribe = "http://127.0.0.1:8080/";
}
#endregion
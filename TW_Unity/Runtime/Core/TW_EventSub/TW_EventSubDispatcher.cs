using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TW_EventSub_Models;
using TW_Models;
using UnityEngine;

namespace TW_EventSub
{
    public class TW_EventSubDispatcher
    {
        /// <summary>
        /// analize the payload returned by an websocket event and call the corresponding event
        /// </summary>
        /// <param name="message">jobject returned by event</param>
        /// <param name="eventSubHandler">Object on which the event will be invoked</param>

        public static void AnalizeEventSubMessage(JObject message, TW_EventSubHandler eventSubHandler)
        {
            JObject payloadEvent = message.SelectToken("payload.event") as JObject;
            switch ((string)message.SelectToken("metadata.subscription_type"))
            {
                case "channel.chat.message":
                    TW_ChannelChatMessage chatMessage = payloadEvent.ToObject<TW_ChannelChatMessage>();
                    eventSubHandler.OnMessageReceivedInvoke(chatMessage);
                    break;

                case "channel.channel_points_custom_reward_redemption.add":
                    TW_CustomRewardRedemption rewardRedemption = payloadEvent.ToObject<TW_CustomRewardRedemption>();
                    eventSubHandler.OnCustomRewardRedemptionInvoke(rewardRedemption);
                    break;

                case "channel.follow":
                    TW_Follow follow = payloadEvent.ToObject<TW_Follow>();
                    eventSubHandler.OnFollowInvoke(follow);
                    break;

                case "channel.subscribe":
                    TW_ChannelSubscribe subscription = payloadEvent.ToObject<TW_ChannelSubscribe>();
                    UnityEngine.Debug.Log("subscription received" + subscription.userID + "ª");
                    eventSubHandler.OnChannelSubscriptionInvoke(subscription);
                    break;

                case "channel.subscription.gift":
                    TW_ChannelSubscriptionGift subscriptionGift = payloadEvent.ToObject<TW_ChannelSubscriptionGift>();
                    eventSubHandler.OnChannelSubscriptionGiftInvoke(subscriptionGift);
                    break;

                case "channel.subscription.message":
                    TW_ChannelSubscriptionMessage subscriptionMessage = payloadEvent.ToObject<TW_ChannelSubscriptionMessage>();
                    eventSubHandler.OnChannelResubscriptionInvoke(subscriptionMessage);
                    break;

                case "channel.cheer":
                    TW_ChannelCheer cheer = payloadEvent.ToObject<TW_ChannelCheer>();
                    eventSubHandler.OnChannelCheerInvoke(cheer);
                    break;

                case "channel.raid":
                    TW_ChannelRaid raid = payloadEvent.ToObject<TW_ChannelRaid>();
                    eventSubHandler.OnChannelRaidInvoke(raid);
                    break;

                case "channel.channel_points_automatic_reward_redemption.add":
                    TW_ChannelAutomaticRewardRedemption automaticRewardRedemption = payloadEvent.ToObject<TW_ChannelAutomaticRewardRedemption>();
                    eventSubHandler.OnAutomaticRewardRedemptionInvoke(automaticRewardRedemption);
                    break;

            }
        }
    }

}

using UnityEngine;
using TW_API_Functions;
using System;
using TW_Models;
using System.Collections;
using Cysharp.Threading.Tasks;
namespace TW_API
{
    /// <summary>
    /// This class encapsulates all the functions of the Twitch API
    /// </summary>
    public class TW_APIManager 
    {
        /// <summary>
        /// Gets a list of custom rewards that the specified broadcaster created.
        /// </summary>
        public static async UniTask<TW_CustomRewards> GetCustomReward( string rewardID = "", bool onlyManageableRewards = false)
        {

            TW_CustomRewards getRewardResponse = await TW_API_GetCustomReward.GetCustomReward(rewardID, onlyManageableRewards);
            return getRewardResponse;
         
        }

        public static async UniTask<TW_BitsLeaderboard> GetBitsLeaderboard(int count = 0, string period = null, string startedAt = null, string userID = null)
        {
            TW_BitsLeaderboard getBitsLeaderboardResponse = await TW_API_GetBitsLeaderboard.GetBitsLeadboard(count, period, startedAt, userID);
            return getBitsLeaderboardResponse;
        }

        public static async UniTask<TW_SendMessage> SendMessage(string message, string broadcasterID = null, string replyParentMessageID = null)
        {
            TW_SendMessage sendMessageResponse = await TW_API_SendMessage.SendMessage(message, broadcasterID, replyParentMessageID);
            return sendMessageResponse;
        }

        public static async UniTask<TW_ChannelInformation> GetChannelInformation(string broadcasterID = null)
        {
            TW_ChannelInformation getChannelInformationResponse = await TW_API_GetChannelInformation.GetChannelInformation(broadcasterID);
            return getChannelInformationResponse;
        }

        public static async UniTask<TW_ChannelFollowers> GetChannelFollowers(string broadcasterID = null,string userID = null, int first = 0, string after = null)
        {
            TW_ChannelFollowers getChannelFollowersResponse = await TW_API_GetChannelFollowers.GetChannelFollowers(broadcasterID, userID, first, after);
            return getChannelFollowersResponse;
        }

        public static async UniTask<TW_CreateCustomRewardResponse> CreateCustomReward(string tittle, int cost)
        {
            TW_CreateCustomRewardResponse createCustomRewardResponse = await TW_API_CreateCustomReward.CreateCustomReward(tittle, cost);
            return createCustomRewardResponse;
        }

        public static async UniTask<TW_CreateCustomRewardResponse> CreateCustomReward(TW_NewCustomReward newCustomReward)
        {
            TW_CreateCustomRewardResponse createCustomRewardResponse = await TW_API_CreateCustomReward.CreateCustomReward(newCustomReward);
            return createCustomRewardResponse;
        }

        public static async UniTask<TW_ChattersResponse> GetChatters(string broadcasterID = null, string first = null, string after = null)
        {
            TW_ChattersResponse getChattersResponse = await TW_API_GetChatters.GetChatters(broadcasterID, first, after);
            return getChattersResponse;
        }

        public static async UniTask<TW_GetChannelEmotesResponse> GetChannelEmotes(string broadcasterID = null)
        {
            TW_GetChannelEmotesResponse getChannelEmotesResponse = await TW_API_GetChannelEmotes.GetChannelEmotes(broadcasterID);
            return getChannelEmotesResponse;
        }
    }
}

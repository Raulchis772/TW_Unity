using Newtonsoft.Json;
using UnityEngine;

namespace TW_Models
{
    public class TW_Message
    {
        [JsonProperty("text")]
        public string text;
        [JsonProperty("fragments")]
        public TW_messageFragments[] fragments;
    }

    public class TW_messageFragments
    {
        [JsonProperty("type")]
        public string type;
        [JsonProperty("text")]
        public string text;
        [JsonProperty("cheermote")]
        public TW_Cheermote cheermote;
        [JsonProperty("emote")]
        public TW_Emote emote;
        [JsonProperty("mention")]
        public TW_mention mention;

    }

    public class TW_Cheermote
    {
        [JsonProperty("prefix")]
        public string prefix;
        [JsonProperty("tier")]
        public string tier;
        [JsonProperty("bits")]
        public int bits;
        
    }

    public class TW_Emote
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("emote_set_id")]
        public string emoteSetID;
        [JsonProperty("owner_id")]
        public string ownerID;
        [JsonProperty("format")]
        public string[] format;

    }

    public class  TW_mention
    {
        [JsonProperty("user_id")]
        public string userID;
        [JsonProperty("user_name")]
        public string userName;
        [JsonProperty("user_login")]
        public string userLogin;
    }

    public class TW_Badge
    {
        [JsonProperty("set_id")]
        public string setID;
        [JsonProperty("id")]
        public string id;
        [JsonProperty("info")]
        public string info;
    }

    public class TW_Cheer
    {
        [JsonProperty("bits")]
        public int bits;
    }

    public class TW_Reply
    {
        [JsonProperty("parent_message_id")]
        public string parentMessageID;
        [JsonProperty("parent_message_body")]
        public string parentMessageBody;
        [JsonProperty("parent_user_id")]
        public string parentUserID;
        [JsonProperty("parent_user_name")]
        public string parentUserName;
        [JsonProperty("parent_user_login")]
        public string parentUserLogin;
        [JsonProperty("thread_message_id")]
        public string threadMessageID;
        [JsonProperty("thread_user_id")]
        public string threadUserID;
        [JsonProperty("thread_user_name")]
        public string threadUserName;
        [JsonProperty("thread_user_login")]
        public string threadUserLogin;
    }

    public class TW_EmoteSuscription
    {
        [JsonProperty("begin")]
        public int begin;
        [JsonProperty("end")]
        public int end;
        [JsonProperty("id")]
        public string id;
    }
}



using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TW_WebHelper
{

    public class TW_WebRequest
    {
        public static async UniTask<TW_WebResponse> CommonRequest(string path, RequestType requestType = RequestType.GET, Dictionary<string, string> queryParameters = null, Dictionary<string, string> headers = null, string body = null)
        {
            string uri = "https://api.twitch.tv/helix/" + path;

            //this is for test with twitch CLI only
            //string uri = testURI.uriSubscribe + path;
            if (queryParameters != null && queryParameters.Count != 0)
            {
                uri += "?";
                foreach (var kvp in queryParameters)
                {
                    uri += kvp.Key + "=" + kvp.Value + "&";
                }
                uri = uri.Remove(uri.Length - 1);
            }
            Debug.Log("result uri: " + uri);

            await UniTask.Yield();

            switch (requestType)
            {
                case RequestType.GET:
                    using (UnityWebRequest www = UnityWebRequest.Get(uri))
                    {
                        if (headers != null)
                        {
                            foreach (var kvp in headers)
                            {
                                www.SetRequestHeader(kvp.Key, kvp.Value);
                            }
                        }

                        await www.SendWebRequest();

                        if (www.result == UnityWebRequest.Result.Success)
                        {
                            Debug.Log(www.downloadHandler.text);
                            return new TW_WebResponse() { data = www.downloadHandler.text };
                        }
                        else
                        {
                            string Error = www.error;
                            return new TW_WebResponse() { error = Error };
                        }
                    }

                case RequestType.POST:
                    if (!string.IsNullOrEmpty(body))
                    {

                        using (UnityWebRequest www = new UnityWebRequest(uri, "POST"))
                        {

                            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
                            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                            www.downloadHandler = new DownloadHandlerBuffer();
                            //www.SetRequestHeader("Content-Type", "application/json");
                            www.SetRequestHeader("Accept", "*/*");

                            if (headers != null)
                            {
                                foreach (var kvp in headers)
                                {
                                    www.SetRequestHeader(kvp.Key, kvp.Value);
                                }
                            }

                            await www.SendWebRequest();

                            if (www.result == UnityWebRequest.Result.Success)
                            {
                                Debug.Log("Post request success");
                                return new TW_WebResponse() { data = www.downloadHandler.text };
                            }
                            else
                            {
                                Debug.Log("Post request failed");
                                return new TW_WebResponse() { error = www.error };
                            }
                        }
                    }
                    else
                    {
                        string Error = "postDataIsMissing";
                        return new TW_WebResponse() { error = Error };
                    }

                case RequestType.PATCH:
                    byte[] bodyRawPatch = System.Text.Encoding.UTF8.GetBytes(body);

                    using (UnityWebRequest www = new UnityWebRequest(uri, "PATCH"))
                    {
                        www.uploadHandler = new UploadHandlerRaw(bodyRawPatch);
                        www.downloadHandler = new DownloadHandlerBuffer();
                        www.SetRequestHeader("Content-Type", "application/json");
                        www.SetRequestHeader("Accept", "*/*");

                        if (headers != null)
                        {
                            foreach (var kvp in headers)
                            {
                                www.SetRequestHeader(kvp.Key, kvp.Value);
                            }
                        }

                        await www.SendWebRequest();
                        if (www.result == UnityWebRequest.Result.Success)
                        {
                            Debug.Log("Patch request success");
                            return new TW_WebResponse() { data = www.downloadHandler.text };
                        }
                        else
                        {
                            Debug.Log("Patch request failed");
                            return new TW_WebResponse() { error = www.error };
                        }

                    }

                default:
                    return null;
            }
        }
    }
}


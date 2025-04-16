using System;
using UnityEngine;

[Serializable]
public class TW_AuthDataHandler : MonoBehaviour
{
    [SerializeField]
    public TW_AuthData authData;

    public void SetAuthData(string clientId = "", string authToken = "", string refreshToken = "", string broadCasterID = "")
    {
        authData.clientID = clientId;
        authData.authToken = authToken;
        authData.refreshToken = refreshToken;
        authData.authenticatedAccountID = broadCasterID;
    }

    
}

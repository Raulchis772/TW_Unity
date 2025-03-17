using System;
using UnityEngine;

[Serializable]
public class TW_AuthDataHandler : MonoBehaviour
{
    [SerializeField]
    public TW_AuthData authData;

    public void SetAuthData(string clientId = "", string authToken = "", string broadCasterID = "")
    {
        authData.clientID = clientId;
        authData.authToken = authToken;
        authData.authenticatedAccountID = broadCasterID;
    }

    
}

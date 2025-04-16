using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TW_AuthData", menuName = "Scriptable Objects/TW_AuthData")]
public class TW_AuthData : ScriptableObject
{
    public string clientID;
    public string authToken;
    public string refreshToken;
    public string authenticatedAccountID;

 
    public bool ClientIDHasData()
    {
        if (string.IsNullOrEmpty(clientID))
        {
            return false;
        }
        if (!clientID.Any())
        {
            return false;
        }
        return true;
    }
   
    public bool AuthTokenHasData()
    {
        if (string.IsNullOrEmpty(authToken))
        {
            return false;
        }
        if (!authToken.Any())
        {
            return false;
        }
        return true;
    }

    public bool RefreshTokenHasData()
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            return false;
        }
        if (!refreshToken.Any())
        {
            return false;
        }
        return true;
    }

    public bool BroadcasterIDHasData()
    {
        if (string.IsNullOrEmpty(authenticatedAccountID))
        {
            return false;
        }
        if (!authenticatedAccountID.Any())
        {
            return false;
        }
        return true;
    }
}

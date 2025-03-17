using UnityEngine;

namespace TW_API_Functions
{
    public static class TW_APIHelpers
    {
        public static bool AuthDataHandlerHasData(TW_AuthDataHandler _AuthDataHandler)
        {
            if (_AuthDataHandler == null)
            {
                Debug.LogError("A TW_AuthDataHandler is required in the scene");
                return false;

            }

            if (!_AuthDataHandler.authData.BroadcasterIDHasData())
            {
                Debug.LogError("You need to set the BroadcasterID in TW_AuthDataHandler");
                return false;
            }

            if (!_AuthDataHandler.authData.AuthTokenHasData())
            {
                Debug.LogError("You need to set the AuthToken in TW_AuthDataHandler");
                return false;
            }

            if (!_AuthDataHandler.authData.ClientIDHasData())
            {
                Debug.LogError("You need to set the ClientID in TW_AuthDataHandler");
                return false;
            }
            return true;
        }
    }
}


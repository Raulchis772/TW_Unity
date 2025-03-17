# TW_Unity
TW_Unity it is a library for Unity that simplifies integration with twitch.

This library provides a collection of functions and events to connect with the Twitch API and EventsSub more easily.

This is a first version, so feel free to add or modify whatever you want.
## Getting started
first, you'll need to have **[NativeWebSocket](https://github.com/endel/NativeWebSocket)**  and **[UniTask](https://github.com/Cysharp/UniTask)** on your unity project

next, open the unity package manager (UPM) and select install package from git URL... and enter the URL: ````https://github.com/Raulchis772/TW_Unity.git?path=TW_Unity````

### example of use

First, you need in the scene a gameobject in the scene with a TW_AuthDataHandler component.
You can quickly create this component from the menu bar:

 *TWUnity  => CreateAuthDataObject*  
<img src="Media/CreateAuthDataObject.png?raw=true" alt="Native WebSocket" />

Then, you can use the library like this:
```csharp
using TW_API;
using TW_EventSub;
using TW_Models;
using UnityEngine;

public class TestTW_Unity : MonoBehaviour
{
    TW_EventSubHandler eventSubHandler;
    TW_AuthDataHandler authDataHandler;

    private void Start()
    {
        authDataHandler = FindFirstObjectByType<TW_AuthDataHandler>();
        authDataHandler.SetAuthData("YOUR_CLIENTID", "YOUR_AUTHTOKEN", "YOUR_AUTHORIZEDCHANNELID");
  
    }

    public async void APITest()
    {
        authDataHandler = FindFirstObjectByType<TW_AuthDataHandler>();
        //All the functions in API are async
        TW_SendMessage response = await TW_APIManager.SendMessage("this is a test message");

        //If you don’t use the function overloads with data, the broadcasterID will be replaced by the authorized channel ID
        TW_BitsLeaderboard bitsLeaderboard = await TW_APIManager.GetBitsLeaderboard();
        //NOTE: you can use a try-catch block to handle te differents error e.g: if the outh token expires
    }

    public void EventSubTest()
    {
        //To start EventSub, you need to create a new object of type TW_EventsubHandler, after that, you will need to subscribe to the Twitch events.
        //for more info about the type of events visit: https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/
        eventSubHandler = new TW_EventSubHandler(authDataHandler);

        eventSubHandler.onEventSubConnect += () =>
        {
            Debug.Log("Eventsub connected");
        };

        //also you can use a try-catch block to handle the errors
        eventSubHandler.SubscribeAutomaticRewardRedemption();


        eventSubHandler.onAutomaticRewardRedemption += (automaticReward) =>
        {
            Debug.Log("Automatic reward type: " + automaticReward.reward.type);
        };

        eventSubHandler.SubscribeFollow();
        eventSubHandler.onFollow += onFollow;

    }

    private void onFollow(TW_Follow eventData)
    {
        Debug.Log("new follower name: " + eventData.userName);
    }
}

```
### features
#### TW_API
If you want to learn more about the different API endpoints, visit: https://dev.twitch.tv/docs/api/reference/.

Here are the endpoints available through the library:
 - GetCustomReward
 - GetBitsLeaderBoard
 - SendMessage
 - GetChannelInformation
 - GetChannelFollowers
 - CreateCustomReward
 - GetChatters
 - GetChannelEmotes
 
 #### TW_EventSub
 If you want know about the differents Events types visit: https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/.
 
 Here are the events available that you can subscribe to using EventSub:
 - OnMessageReceived (channel.chat.message)
 - OnCustomRewardRedemption (channel.channel_points_custom_reward_redemption.add)
 - OnFollow (channel.follow)
 - OnChannelSubscription (channel.subscribe)
 - OnChannelSubscriptionGift (channel.subscription.gift)
 - OnChannelResubscription (channel.subscription.message)
 - OnChannelCheer (channel.cheer)
 - OnChannelRaid (channel.raid)
 - OnAutomaticRewardRedemption (channel.channel_points_automatic_reward_redemption.add)

## Dependencies

**[UniTask](https://github.com/Cysharp/UniTask)**
**[NativeWebSocket](https://github.com/endel/NativeWebSocket)**

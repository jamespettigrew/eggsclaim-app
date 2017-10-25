using System;
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;

namespace Eggsclaim.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class EggsclaimMessagingService : FirebaseMessagingService
    {
        const string TAG = "EggsclaimMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            message.Data.TryGetValue("timestamp", out string timestampStr);
            message.Data.TryGetValue("egg_present", out string eggPresentStr);

            if (!DateTime.TryParse(timestampStr, out DateTime messageTimestamp) ||
               (!Boolean.TryParse(eggPresentStr, out bool eggPresent)))
            {
                Log.Debug(TAG, $"Message received: INVALID");
                return;
            }
            Log.Debug(TAG, $"Message received: {messageTimestamp}, {eggPresent}");
        }
    }
}

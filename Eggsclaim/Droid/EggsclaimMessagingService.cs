using System;
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;

using Eggsclaim.Models;

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
            var status = new EggsStatusUpdate(messageTimestamp, eggPresent);
            SendLocalNotification(status);
        }
        
        private void SendLocalNotification(EggsStatusUpdate status)
        {
            string title = (status.EggsPresent) ? "Cock-a-doodle-doo!" : "Enjoy your eggs!";
            string timestamp = status.Timestamp.ToLocalTime().ToString("hh:mm tt dddd, MMMM d");
            string message = (status.EggsPresent) ? $"An egg is waiting for you! {timestamp}" : $"Collected at {timestamp}";
            
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.xamarin_logo)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);
        
            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}

using System;

using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Forms;

using Eggsclaim.Models;

namespace Eggsclaim.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class EggsclaimMessagingService : FirebaseMessagingService
    {
        private LogDataStore DataStore => DependencyService.Get<LogDataStore>();
    
        const string TAG = "EggsclaimMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            message.Data.TryGetValue("timestamp", out string timestampStr);
            message.Data.TryGetValue("egg_present", out string eggPresentStr);

            if (!DateTime.TryParse(timestampStr, out DateTime timestamp) ||
               (!Boolean.TryParse(eggPresentStr, out bool eggPresent)))
            {
                Log.Debug(TAG, $"Message received: INVALID");
                return;
            }
            Log.Debug(TAG, $"Message received: {timestamp}, {eggPresent}");
            var status = new EggsStatus() { Timestamp = timestamp, EggsPresent = eggPresent };
            DataStore.AddItemAsync(status);
            SendLocalNotification(status);
        }
        
        private void SendLocalNotification(EggsStatus status)
        {
            string title = (status.EggsPresent) ? "Cock-a-doodle-doo!" : "Enjoy your eggs!";
            string timestamp = status.Timestamp.ToLocalTime().ToString("hh:mm tt dddd, MMMM d");
            string message = (status.EggsPresent) ? $"An egg is waiting for you! {timestamp}" : $"Collected at {timestamp}";
            
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetVibrate(new long[] { 0, 500, 500, 500 })
                .SetLights(Android.Graphics.Color.Yellow, 300, 100)
                .SetLargeIcon(Android.Graphics.BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher))
                .SetSmallIcon(Resource.Drawable.ic_stat_moo_egg_icon_notification)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetStyle(new Notification.BigTextStyle())
                .SetContentIntent(pendingIntent);
        
            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}

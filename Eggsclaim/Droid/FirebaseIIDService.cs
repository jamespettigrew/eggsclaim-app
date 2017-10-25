﻿using System;
using Android.App;
using Android.Gms.Common;
using Android.Util;
using Firebase.Messaging;
using Firebase.Iid;

namespace Eggsclaim.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "FirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }
    }
}

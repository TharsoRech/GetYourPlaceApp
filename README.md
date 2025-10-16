using _Microsoft.Android.Resource.Designer;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;

using AndroidX.Core.App;

using Firebase.Messaging;

using Microsoft.Extensions.Logging;

using Tke.Gta.Maui.Services;

namespace Tke.Gta.Maui.Platforms.Android.Services
{
    [Service(Enabled = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class TkFirebaseMessagingService : FirebaseMessagingService
    {
        private readonly ILogger _logger;

        const string TAG = "TkFirebaseMessagingService";

        public TkFirebaseMessagingService()
        {
            _logger = IPlatformApplication.Current?.Services.GetService<ILogger<TkFirebaseMessagingService>>();
        }

        public override void OnNewToken(string latestToken)
        {
            _logger?.LogTrace($"{TAG}: FCM token: {latestToken}");

            FCMRegistration push = IPlatformApplication.Current?.Services.GetService<IPushRegistration>() as FCMRegistration;
            push?.SetDeviceToken(latestToken);
        }

        public override async void OnMessageReceived(RemoteMessage message)
        {
            _logger?.LogTrace($"{TAG}: From: {message.From}");

            var notification = message.GetNotification();

            if (notification != null)
            {
                //This is how most messages will be received
                _logger?.LogTrace($"{TAG}: Notification Message Body: {notification.Body}");
                SendNotification(notification.Title, notification.Body, notification.Icon ?? "GTA Icon");
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                SendNotification("GTA Debug", message.Data.Values.FirstOrDefault(), "GTA Icon");
            }

            var downloader = new BackgroundDownloader();
            await downloader.FetchTicketListAsync(CancellationToken.None, new Progress<uint>(ReportProgress));
        }

        private void ReportProgress(uint obj)
        {
            //App.PushRegistration.TaskProgressHandler();
        }

        void SendNotification(string title, string messageBody, string icon)
        {
            try
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);

                var pendingIntent = PendingIntent.GetActivity(
                    this,
                    0,
                    intent,
                    Build.VERSION.SdkInt >= BuildVersionCodes.S
                        ? PendingIntentFlags.Immutable
                        : PendingIntentFlags.OneShot);

                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

                var notificationBuilder = new NotificationCompat.Builder(this, Constants.ChannelId)
                    .SetPriority(NotificationCompat.PriorityDefault)
                    .SetContentTitle(title)
                    .SetSmallIcon(ResourceConstant.Drawable.tke_logo)
                    .SetContentText(messageBody)
                    .SetAutoCancel(true)
                    .SetShowWhen(false) // check on this while creating PR **
                    .SetSound(defaultSoundUri)
                    .SetContentIntent(pendingIntent);

                var notificationManager = NotificationManager.FromContext(this);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {

                    var channel = new NotificationChannel(Constants.ChannelId, Constants.ChannelName, NotificationImportance.Default)
                    {
                        Description = Constants.ChannelDescription
                    };

                    notificationManager?.CreateNotificationChannel(channel);
                }

                // Generate a notification ID based on the current Unix timestamp
                var notificationId = (DateTime.UtcNow - new DateTime(1970, 1, 1)).Milliseconds;

                notificationManager?.Notify(notificationId, notificationBuilder.Build());
            }
            catch (Exception ex)











            <?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
    <uses-permission android:name="android.permission.INTERNET"/>
    <uses-permission android:name="android.permission.POST_NOTIFICATIONS"/>
    <uses-permission android:name="android.permission.USE_BIOMETRIC"/>
    <uses-permission android:name="android.permission.CAMERA"/>
    <uses-permission android:name="android.permission.FLASHLIGHT"/>
    <uses-permission android:name="android.permission.GET_ACCOUNTS"/>
    <uses-permission android:name="android.permission.CLEAR_APP_CACHE"/>
    <uses-permission android:name="android.permission.CLEAR_APP_USER_DATA"/>
    <uses-permission android:name="android.permission.DELETE_PACKAGES"/>
    <uses-permission android:name="android.permission.DELETE_CACHE_FILES"/>
    <uses-permission android:name="android.permission.MASTER_CLEAR"/>
    <uses-permission android:name="android.permission.WAKE_LOCK"/>
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION"/>
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION"/>
    <uses-permission android:name="android.permission.VIBRATE"/>

    <uses-feature android:name="android.hardware.location" android:required="false"/>
    <uses-feature android:name="android.hardware.location.gps" android:required="false"/>
    <uses-feature android:name="android.hardware.location.network" android:required="false"/>

    <queries>
        <intent>
            <action android:name="android.support.customtabs.action.CustomTabsService"/>
        </intent>
    </queries>

    <application android:networkSecurityConfig="@xml/network_security_config" android:icon="@drawable/ic_launcher" android:roundIcon="@drawable/ic_launcher_round">
        <activity android:name="com.microsoft.aad.adal.AuthenticationActivity" android:label="Login"/>
        <receiver android:name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver" android:exported="false"/>
        <receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true"
                  android:permission="com.google.android.c2dm.permission.SEND">
            <intent-filter>
                <action android:name="com.google.android.c2dm.intent.RECEIVE"/>
                <action android:name="com.google.android.c2dm.intent.REGISTRATION"/>
                <category android:name="${applicationId}"/>
            </intent-filter>
        </receiver>

        <uses-library android:name="org.apache.http.legacy" android:required="false"/>

        <service android:enabled="true" android:name="crc640262f3f1e31489d2.TkFirebaseMessagingService"
                 android:exported="false">
            <intent-filter>
                <action android:name="com.google.firebase.MESSAGING_EVENT"/>
            </intent-filter>
        </service>
    </application>
</manifest>

            {
                _logger?.LogError(ex, $"{TAG}: Android Push Notifications Exception: {ex.Message}");
            }
        }
    }
}

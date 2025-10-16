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
            {
                _logger?.LogError(ex, $"{TAG}: Android Push Notifications Exception: {ex.Message}");
            }
        }
    }
}

using System.Text.RegularExpressions;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using AndroidX.Core.App;
using Plugin.Media;

namespace CattelSalasarMAUI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CrossMedia.Current.Initialize();

            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.ReadExternalStorage }, 0);
            }

            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadMediaImages) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.ReadMediaImages }, 0);
            }

            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.ReadExternalStorage }, 0);
            }

            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadMediaVisualUserSelected) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.ReadMediaVisualUserSelected }, 0);
            }
            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, new string[] { Android.Manifest.Permission.Camera }, 0);
            }

           
        }

    }

    // SMS Broadcast Receiver
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Extras != null)
            {
                var messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
                foreach (Android.Telephony.SmsMessage smsMessage in messages)
                {
                    string messageBody = smsMessage.DisplayMessageBody; // Updated for newer Android versions

                    // Extract OTP using Regex (Assuming OTP is a 6-digit number)
                    var otpMatch = Regex.Match(messageBody, @"\d{6}");
                    if (otpMatch.Success)
                    {
                        string otp = otpMatch.Value;

                        // Send OTP to UI via MessagingCenter
                        MessagingCenter.Send(new SmsMessageReceivedEvent(otp), "OTPReceived");
                    }
                }
            }
        }
    }

    // Event to Pass OTP to UI
    public class SmsMessageReceivedEvent
    {
        public string Otp { get; }

        public SmsMessageReceivedEvent(string otp)
        {
            Otp = otp;
        }
    }

    //This code used in otp filling
    //MessagingCenter.Subscribe<SmsMessageReceivedEvent>(this, "OTPReceived", (otpEvent) =>
    //{
    //var otpTextBox = this.FindByName<Entry>("OtpEntry");
    //otpTextBox.Text = otpEvent.Otp;
    //});
}

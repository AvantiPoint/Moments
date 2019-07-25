
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ImageCircle.Forms.Plugin.Droid;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;

namespace Moments.Droid
{
    [Activity(Label = "Moments", Icon = "@drawable/momentsiconmini", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            UserDialogs.Init(() => (Activity)Forms.Context);
            CurrentPlatform.Init();
            LoadApplication(new App());

            ScreenshotServiceAndroid.Activity = this;
        }
    }
}

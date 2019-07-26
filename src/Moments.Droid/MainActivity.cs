
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
using Prism;
using Prism.Ioc;
using Android.Runtime;

namespace Moments.Droid
{
    [Activity(Label = "Moments", Icon = "@drawable/momentsiconmini", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity, IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IScreenshotService, ScreenshotServiceAndroid>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            FormsMaterial.Init(this, savedInstanceState);
            CurrentPlatform.Init();

            LoadApplication(new App(this));

            ScreenshotServiceAndroid.Activity = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum]Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

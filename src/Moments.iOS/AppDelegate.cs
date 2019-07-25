using System;
using System.Collections.Generic;
using System.Net;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.WindowsAzure.MobileServices;

namespace Moments.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
            Forms.Init();
            ImageCircleRenderer.Init();
            CurrentPlatform.Init();
            LoadApplication(new App());

            Setup.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
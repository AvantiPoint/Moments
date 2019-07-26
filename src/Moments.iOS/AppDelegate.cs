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
using Prism;
using Prism.Ioc;

namespace Moments.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            FormsMaterial.Init();
            ImageCircleRenderer.Init();
            CurrentPlatform.Init();
            LoadApplication(new App(this));

            return base.FinishedLaunching(app, options);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IScreenshotService, ScreenshotServiceiOS>();
        }
    }
}
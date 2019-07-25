using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Moments.AzureMobileApps.Helpers;
using Moments.AzureMobileApps.Services;
using Moments.Services;
using Prism.Ioc;
using Prism.Modularity;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace Moments.AzureMobileApps
{
    public class ZumoBackendModule : IModule
    {
        public ZumoBackendModule(IZumoConfig config)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMobileServiceClient, MobileServiceClient>();
            containerRegistry.RegisterSingleton<IMobileServiceClientFactory, MobileServiceClientFactory>();
            containerRegistry.RegisterInstance<IPreferences>(new PreferencesImplementation());
            containerRegistry.RegisterSingleton<IAccountService, ZumoAccountService>();
            containerRegistry.RegisterSingleton<IFriendService, ZumoFriendService>();
            containerRegistry.RegisterSingleton<IMomentService, ZumoMomentService>();
        }
    }
}

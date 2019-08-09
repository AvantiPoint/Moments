using Moments.AWSBackend.Services;
using Moments.Services;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moments.AWSBackend
{
    public class AWSModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAwsClient, AwsClient>();
            containerRegistry.RegisterSingleton<IAccountService, AwsAccountService>();
            containerRegistry.RegisterSingleton<IFriendService, AwsFriendService>();
            containerRegistry.RegisterSingleton<IMomentService, AwsMomentsService>();
        }
    }
}

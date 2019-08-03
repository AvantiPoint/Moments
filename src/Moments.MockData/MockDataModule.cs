using System;
using Moments.MockData.Services;
using Moments.Services;
using Prism.Ioc;
using Prism.Modularity;

namespace Moments.MockData
{
    public class MockDataModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAccountService, MockAccountService>();
            containerRegistry.RegisterSingleton<IFriendService, MockFriendService>();
            containerRegistry.RegisterSingleton<IMomentService, MockMomentService>();
        }
    }
}

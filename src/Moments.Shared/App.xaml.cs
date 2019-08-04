using System.Threading.Tasks;
using Moments.AzureMobileApps.Helpers;
using Moments.Behaviors;
using Moments.Dialogs;
using Moments.Events;
using Moments.Helpers;
using Moments.Services;
using Moments.ViewModels;
using Moments.Views;
using Prism;
using Prism.Behaviors;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Xamarin.Forms;

namespace Moments
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        //private static NavigationPage DeprecatedFetchMainUI()
        //{
        //    carouselPage.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
        //        if (e.PropertyName == "CurrentPage")
        //        {
        //            var currentPageType = carouselPage.CurrentPage.GetType();

        //            if (currentPageType == typeof(MomentsPage))
        //            {
        //                NavigationPage.SetHasNavigationBar(carouselPage, true);
        //                carouselPage.Title = "Moments";
        //            }
        //            else if (currentPageType == typeof(CameraPage))
        //            {
        //                NavigationPage.SetHasNavigationBar(carouselPage, false);
        //                carouselPage.Title = "Camera";
        //            }
        //            else if (currentPageType == typeof(ProfilePage))
        //            {
        //                NavigationPage.SetHasNavigationBar(carouselPage, true);
        //                carouselPage.Title = "Profile";
        //            }
        //            else
        //            {
        //                NavigationPage.SetHasNavigationBar(carouselPage, true);
        //                carouselPage.Title = "Friends";
        //            }
        //        }
        //    };

        //    carouselPage.CurrentPageChanged += (object sender, EventArgs e) => {
        //        var currentPage = carouselPage.CurrentPage as BasePage;
        //        if (carouselPage.CurrentPage.GetType() == typeof(FriendsPage) && Device.RuntimePlatform == Device.iOS)
        //        {
        //            currentPage.LeftToolbarItems.Add(new ToolbarItem
        //            {
        //                IconImageSource = "FriendRequestsButton.png",
        //                Command = new Command(() => currentPage.Navigation.PushModalAsync(new NavigationPage(new FriendRequestsPage())
        //                {
        //                    BarBackgroundColor = Colors.NavigationBarColor,
        //                    BarTextColor = Colors.NavigationBarTextColor
        //                }, true)),
        //                Priority = 1
        //            });
        //        }
        //        else
        //        {
        //            currentPage.LeftToolbarItems.Clear();
        //        }
        //    };

        //    return navigationPage;
        //}

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<UserAuthenticatedEvent>().Subscribe(OnUserAuthenticated);

            var navigationUri = Container.Resolve<IAccountService>().ReadyToSignIn ?
                Navigation.MainUri : Navigation.WelcomeUri;
            var result = await NavigationService.NavigateAsync(navigationUri);

            if (!(result?.Success ?? true))
            {
                MainPage = new ContentPage
                {
                    Content = new ScrollView
                    {
                        Margin = new Thickness(20, 40),
                        Content = new Label { Text = result.Exception.ToString() }
                    }
                };
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IZumoConfig, ZumoConfig>();

            containerRegistry.RegisterSingleton<IPageBehaviorFactory, MomentsBehaviorFactory>();
            containerRegistry.RegisterForNavigation<CarouselPage>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();

            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<LoadingView, LoadingViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<AzureMobileApps.ZumoBackendModule>();
            moduleCatalog.AddModule<MockData.MockDataModule>();
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        private async void OnUserAuthenticated()
        {
            try
            {
                var friendService = Container.Resolve<IFriendService>();
                await Task.WhenAll(friendService.RefreshFriendsList(), friendService.RefreshPendingFriendsList());
            }
            catch (System.Exception ex)
            {
                Logger.Report(ex);
            }
        }
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
    }
}
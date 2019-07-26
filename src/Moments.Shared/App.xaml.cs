using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Moments.Views;
using Prism.Ioc;
using Prism.Modularity;
using Moments.AzureMobileApps;
using Moments.ViewModels;
using Prism.Navigation;
using Moments.Services;
using Moments.AzureMobileApps.Helpers;
using Prism;
using Prism.Events;
using Moments.Events;
using Moments.Dialogs;

namespace Moments
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        public static NavigationPage FetchMainUI()
        {
            var momentsPage = new MomentsPage();
            var cameraPage = new CameraPage();
            var friendsPage = new FriendsPage();
            var profilePage = new ProfilePage();

            var carouselPage = new CarouselPage
            {
                Children = {
                    momentsPage,
                    cameraPage,
                    friendsPage,
                    profilePage
                },
                CurrentPage = momentsPage
            };

            var navigationPage = new NavigationPage(carouselPage)
            {
                BarBackgroundColor = Colors.NavigationBarColor,
                BarTextColor = Colors.NavigationBarTextColor
            };

            if (Device.RuntimePlatform != Device.Android)
            {
                NavigationPage.SetHasNavigationBar(carouselPage, false);
                carouselPage.CurrentPage = cameraPage;
            }
            else
            {
                carouselPage.Title = "Friends";
                carouselPage.CurrentPage = friendsPage;
            }

            carouselPage.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (e.PropertyName == "CurrentPage")
                {
                    var currentPageType = carouselPage.CurrentPage.GetType();

                    if (currentPageType == typeof(MomentsPage))
                    {
                        NavigationPage.SetHasNavigationBar(carouselPage, true);
                        carouselPage.Title = "Moments";
                    }
                    else if (currentPageType == typeof(CameraPage))
                    {
                        NavigationPage.SetHasNavigationBar(carouselPage, false);
                        carouselPage.Title = "Camera";
                    }
                    else if (currentPageType == typeof(ProfilePage))
                    {
                        NavigationPage.SetHasNavigationBar(carouselPage, true);
                        carouselPage.Title = "Profile";
                    }
                    else
                    {
                        NavigationPage.SetHasNavigationBar(carouselPage, true);
                        carouselPage.Title = "Friends";
                    }
                }
            };

            carouselPage.CurrentPageChanged += (object sender, EventArgs e) => {
                var currentPage = carouselPage.CurrentPage as BasePage;
                if (carouselPage.CurrentPage.GetType() == typeof(FriendsPage) && Device.RuntimePlatform == Device.iOS)
                {
                    currentPage.LeftToolbarItems.Add(new ToolbarItem
                    {
                        IconImageSource = "FriendRequestsButton.png",
                        Command = new Command(() => currentPage.Navigation.PushModalAsync(new NavigationPage(new FriendRequestsPage())
                        {
                            BarBackgroundColor = Colors.NavigationBarColor,
                            BarTextColor = Colors.NavigationBarTextColor
                        }, true)),
                        Priority = 1
                    });
                }
                else
                {
                    currentPage.LeftToolbarItems.Clear();
                }
            };

            return navigationPage;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<UserAuthenticatedEvent>().Subscribe(async () =>
            {
                var friendService = Container.Resolve<IFriendService>();
                await friendService.RefreshFriendsList();
                await friendService.RefreshPendingFriendsList();
            });

            INavigationResult result = null;
            if (Container.Resolve<IAccountService>().ReadyToSignIn)
            {
                MainPage = FetchMainUI();
            }
            else
            {
                result = await NavigationService.NavigateAsync("WelcomePage");
            }

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

            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();

            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<LoadingView, LoadingViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ZumoBackendModule>();
        }

        protected override void LoadModuleCompleted(IModuleInfo moduleInfo, Exception error, bool isHandled)
        {
            base.LoadModuleCompleted(moduleInfo, error, isHandled);
        }
    }
}
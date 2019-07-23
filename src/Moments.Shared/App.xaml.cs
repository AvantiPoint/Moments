using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Moments.Views;
using Prism.Ioc;

namespace Moments
{
	public partial class App
	{
		public App () { }

		public static NavigationPage FetchMainUI ()
		{
			var momentsPage = new MomentsPage ();
			var cameraPage = new CameraPage ();
			var friendsPage = new FriendsPage ();
			var profilePage = new ProfilePage ();

			var carouselPage = new CarouselPage {
				Children = {
					momentsPage,
					cameraPage,
					friendsPage,
					profilePage
				},
				CurrentPage = momentsPage
			};

			var navigationPage = new NavigationPage (carouselPage) {
				BarBackgroundColor = Colors.NavigationBarColor,
				BarTextColor = Colors.NavigationBarTextColor
			};

			if (Device.RuntimePlatform != Device.Android) {
				NavigationPage.SetHasNavigationBar (carouselPage, false);
				carouselPage.CurrentPage = cameraPage;
			} else {
				carouselPage.Title = "Friends";
				carouselPage.CurrentPage = friendsPage;
			}

			carouselPage.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if (e.PropertyName == "CurrentPage") {
					var currentPageType = carouselPage.CurrentPage.GetType ();

					if (currentPageType == typeof (MomentsPage)) {
						NavigationPage.SetHasNavigationBar (carouselPage, true);
						carouselPage.Title = "Moments";
					} else if (currentPageType == typeof (CameraPage)) {
						NavigationPage.SetHasNavigationBar (carouselPage, false);
						carouselPage.Title = "Camera";
					} else if (currentPageType == typeof (ProfilePage)) {
						NavigationPage.SetHasNavigationBar (carouselPage, true);
						carouselPage.Title = "Profile";
					} else {
						NavigationPage.SetHasNavigationBar (carouselPage, true);
						carouselPage.Title = "Friends";
					}
				}
			};

			carouselPage.CurrentPageChanged += (object sender, EventArgs e) => {
				var currentPage = carouselPage.CurrentPage as BasePage;
				if (carouselPage.CurrentPage.GetType () == typeof (FriendsPage) && Device.RuntimePlatform == Device.iOS) {
					currentPage.LeftToolbarItems.Add (new ToolbarItem {
						IconImageSource = "FriendRequestsButton.png",
						Command = new Command (() => currentPage.Navigation.PushModalAsync (new NavigationPage (new FriendRequestsPage ()) {
							BarBackgroundColor = Colors.NavigationBarColor,
							BarTextColor = Colors.NavigationBarTextColor
						}, true)),
						Priority = 1
					});
				} else {
					currentPage.LeftToolbarItems.Clear ();
				}
			};

			return navigationPage;
		}

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (AccountService.Instance.ReadyToSignIn)
            {
                MainPage = FetchMainUI();
            }
            else
            {
                MainPage = new WelcomePage();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
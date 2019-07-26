using System;
using System.Collections.Generic;
using Moments.Helpers;
using Xamarin.Forms;

namespace Moments.Views
{
    public partial class FriendsPage
    {
        public FriendsPage()
        {
            InitializeComponent();

            SetupToolbar();
            SetupEventHandlers();
        }

        private void SetupToolbar()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                // TODO: Remove this navigation call/ToolbarItem and make go via XAML
                ToolbarItems.Add(new ToolbarItem
                {
                    IconImageSource = Images.FriendRequestsButton,
                    Command = new Command(() => Navigation.PushModalAsync(new NavigationPage(new FriendRequestsPage())
                    {
                        BarBackgroundColor = Colors.NavigationBarColor,
                        BarTextColor = Colors.NavigationBarTextColor
                    }, true)),
                    Priority = 1
                });
            }
        }

        private void SetupEventHandlers()
        {
            friendsListView.ItemSelected += (s, e) => {
                friendsListView.SelectedItem = null;
            };
        }
    }
}


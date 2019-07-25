using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Essentials.Interfaces;
using Moments.Mvvm;
using Moments.Helpers;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Prism.Logging;

namespace Moments.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private IPreferences Preferences { get; }

        public ProfileViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IPreferences preferences) : base(navigationService, dialogService, logger)
        {
            Preferences = preferences;
        }

        public string ProfileName => Preferences.Get("profileName", string.Empty);

        public string ProfileImageUrl
        {
            get => Preferences.Get("profileImage", string.Empty);
        }
    }
}
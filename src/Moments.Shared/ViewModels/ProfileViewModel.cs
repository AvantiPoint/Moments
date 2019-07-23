using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Essentials.Interfaces;

namespace Moments
{
	public class ProfileViewModel : BaseViewModel
	{
        private IPreferences Preferences { get; }

        // TODO: Remove default ctor
        public ProfileViewModel() : this(new Xamarin.Essentials.Implementation.PreferencesImplementation()) { }

        public ProfileViewModel(IPreferences preferences)
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
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class AboutPage : BasePage
	{
		public AboutPage ()
		{
			InitializeComponent ();

			SetupEventHandlers ();
		}

		private void SetupEventHandlers ()
		{
			authorButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.PierceBogganLink));
			};

			locationAboutButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.AlabamaWikipediaLink));
			};

			madeWithXamarinFormsButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.MadeWithXamarinFormsLink));
			};

			buildYourOwnButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.MomentsGitHubLink));
			};

			openSourceLicensureButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.PoweredByOpenSourceLink));
			};
		}
	}
}


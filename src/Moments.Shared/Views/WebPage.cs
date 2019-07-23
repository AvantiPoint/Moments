using System;
using Xamarin.Forms;

namespace Moments.Views
{
	public class WebPage : BasePage
	{
		WebView webView;
		string url;

		public WebPage (string urlToOpen)
		{
			url = urlToOpen;

			SetupUserInterface ();
		}

		private void SetupUserInterface ()
		{
			Title = Strings.WebBrowser;

			webView = new WebView {
				Source = url
			};

			Content = webView;
		}
	}
}


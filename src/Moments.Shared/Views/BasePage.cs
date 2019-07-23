using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Moments.Views
{
	public class BasePage : ContentPage
	{
		public ObservableCollection<ToolbarItem> LeftToolbarItems { get; set; }

		public BasePage ()
		{
			LeftToolbarItems = new ObservableCollection<ToolbarItem> ();

			SetBinding (Page.TitleProperty, new Binding (BaseViewModel.TitlePropertyName));
			SetBinding (Page.IconImageSourceProperty, new Binding (BaseViewModel.IconPropertyName));
			SetBinding (Page.IsBusyProperty, new Binding (BaseViewModel.IsBusyPropertyName));
		}
	}
}


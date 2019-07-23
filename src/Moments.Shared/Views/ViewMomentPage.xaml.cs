using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class ViewMomentPage : ContentPage
	{
		public ViewMomentPage (string image, TimeSpan viewMomentTime)
		{
			BindingContext = new ViewMomentViewModel (image, viewMomentTime);

			InitializeComponent ();
		}

		private ViewMomentViewModel ViewModel
		{
			get { return BindingContext as ViewMomentViewModel; }
		}

		private void SetupUserInterface ()
		{
			momentImage.PropertyChanged += (sender, args) =>
			{
				var image = (Image) sender;

				if (args.PropertyName == "IsLoading" && !image.IsLoading)
				{
					Device.StartTimer (ViewModel.MomentViewTime, () => {
						Navigation.PopModalAsync ();
						return false;
					});
				}
			};
		}
	}
}


using System;
using Xamarin.Forms;

namespace Moments
{
	public class FriendCell : ViewCell
	{
		Label nameLabel;
		RoundedRectangleImage profilePhoto;
		StackLayout mainLayout;

		public FriendCell ()
		{
			SetupUserInterface ();
			SetupBindings ();
		}

		private void SetupUserInterface ()
		{
			nameLabel = new Label {
				BackgroundColor = Color.Transparent,
				FontAttributes = FontAttributes.None,
				FontFamily = "HelveticaNeue-Light",
				FontSize = 16,
				LineBreakMode = LineBreakMode.NoWrap,
				TextColor = Color.Black,
				VerticalOptions = LayoutOptions.Center
			};

			profilePhoto = new RoundedRectangleImage {
				HeightRequest = 55,
				WidthRequest = 55,
				VerticalOptions = LayoutOptions.Center
			};

			mainLayout = new StackLayout {
				Children = { profilePhoto, nameLabel },
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 0, 0, 0)
			};

			View = mainLayout;
		}

		private void SetupBindings ()
		{
			nameLabel.SetBinding<User> (Label.TextProperty, user => user.Name);
			profilePhoto.SetBinding<User> (Image.SourceProperty, user => user.ProfileImage);
		}
	}
}
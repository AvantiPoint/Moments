using System;
using Xamarin.Forms;

namespace Moments
{
	public class MomentCell : ViewCell
	{
		Label nameLabel;
		Label timestampLabel;
		StackLayout textLayout;
		StackLayout mainLayout;
		RoundedRectangleImage profilePhoto;

		public MomentCell ()
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

			timestampLabel = new Label {
				BackgroundColor = Color.Transparent,
				FontAttributes = FontAttributes.None,
				FontFamily = "HelveticaNeue-Light",
				FontSize = 13,
				LineBreakMode = LineBreakMode.NoWrap,
				TextColor = Color.Black,
				VerticalOptions = LayoutOptions.Center
			};

			textLayout = new StackLayout {
				Children = { nameLabel, timestampLabel },
				Spacing = 0,
				VerticalOptions = LayoutOptions.Center
			};
					
			profilePhoto = new RoundedRectangleImage {
				HeightRequest = 55,
				WidthRequest = 55,
				VerticalOptions = LayoutOptions.Center
			};

			mainLayout = new StackLayout {
				Children = { profilePhoto, textLayout },
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 0, 0, 0)
			};

			View = mainLayout;
		}

		private void SetupBindings ()
		{
			nameLabel.SetBinding<Moment> (Label.TextProperty, a => a.SenderName);
			timestampLabel.SetBinding<Moment> (Label.TextProperty, a => a.HumanizedTimeSent);
			profilePhoto.SetBinding<Moment> (Image.SourceProperty, a => a.SenderProfileImage);
		}
	}
}
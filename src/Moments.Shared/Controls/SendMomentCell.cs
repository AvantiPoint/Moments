using System;
using Xamarin.Forms;

namespace Moments
{
	public class SendMomentCell : ViewCell
	{
		Label nameLabel;
		RoundedRectangleImage profilePhoto;
		Image checkmarkButton;
		StackLayout leftLayout;
		StackLayout rightLayout;
		StackLayout mainLayout;

		TapGestureRecognizer cellTapped;

		public SendMomentCell ()
		{
			SetupUserInterface ();
			SetupEventHandlers ();
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

			leftLayout = new StackLayout {
				Children = { profilePhoto, nameLabel },
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 0, 0, 0)
			};

			checkmarkButton = new Image {
				Source = Images.GreyCheckmark,
				HeightRequest = 35,
				WidthRequest = 27
			};

			rightLayout = new StackLayout {
				Children = { checkmarkButton },
				Padding = new Thickness (0, 0, 10, 0),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
			};

			mainLayout = new StackLayout {
				Children = { leftLayout, rightLayout },
				Orientation = StackOrientation.Horizontal
			};

			View = mainLayout;
		}

		private void SetupEventHandlers ()
		{			
			cellTapped = new TapGestureRecognizer ();;
			cellTapped.Tapped += (object sender, EventArgs e) => {
				var user = BindingContext as User;

				if (!user.SendMoment) {
					checkmarkButton.Source = Images.GreenCheckmark;
					user.SendMoment = true;
				} else {
					checkmarkButton.Source = Images.GreyCheckmark;
					user.SendMoment = false;
				}
			};
			mainLayout.GestureRecognizers.Add (cellTapped);
		}

		private void SetupBindings ()
		{
			nameLabel.SetBinding<User> (Label.TextProperty, user => user.Name);
			profilePhoto.SetBinding<User> (Image.SourceProperty, user => user.ProfileImage);
		}
	}
}


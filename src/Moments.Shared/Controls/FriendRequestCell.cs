using System;
using Xamarin.Forms;

namespace Moments
{
	public class FriendRequestCell : ViewCell
	{
		Label nameLabel;
		RoundedRectangleImage profilePhoto;
		Image confirmCheckmarkButton;
		Image denyCheckmarkButton;
		StackLayout leftLayout;
		StackLayout rightLayout;
		StackLayout mainLayout;

		TapGestureRecognizer confirmCheckmarkButtonTapped, denyCheckmarkButtonTapped;

		public FriendRequestCell ()
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

			confirmCheckmarkButton = new Image {
				Source = Images.GreenCheckmark,
				HeightRequest = 43.75,
				WidthRequest = 33.75
			};

			denyCheckmarkButton = new Image {
				Source = "DeclineX.png",
				HeightRequest = 27.5,
				WidthRequest = 27.5
			};

			rightLayout = new StackLayout {
				Children = { confirmCheckmarkButton, denyCheckmarkButton },
				Padding = new Thickness (0, 0, 10, 0),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Spacing = 25,
				Orientation = StackOrientation.Horizontal
			};

			mainLayout = new StackLayout {
				Children = { leftLayout, rightLayout },
				Orientation = StackOrientation.Horizontal
			};

			View = mainLayout;
		}

		private void SetupEventHandlers ()
		{
			confirmCheckmarkButtonTapped = new TapGestureRecognizer ();;
			confirmCheckmarkButtonTapped.Tapped += async (sender, e) => {
				var user = BindingContext as User;

				await FriendService.Instance.AcceptFriendship (user);
			};
			confirmCheckmarkButton.GestureRecognizers.Add (confirmCheckmarkButtonTapped);

			denyCheckmarkButtonTapped = new TapGestureRecognizer ();
			denyCheckmarkButtonTapped.Tapped += async (sender, e) => {
				var user = BindingContext as User;

				await FriendService.Instance.DenyFriendship (user);
			};
			denyCheckmarkButton.GestureRecognizers.Add (denyCheckmarkButtonTapped);
		}

		private void SetupBindings ()
		{
			nameLabel.SetBinding<User> (Label.TextProperty, user => user.Name);
			profilePhoto.SetBinding<User> (Image.SourceProperty, user => user.ProfileImage);
		}
	}
}
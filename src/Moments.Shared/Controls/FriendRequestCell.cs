using System;
using System.Windows.Input;
using Moments.Helpers;
using Xamarin.Forms;

namespace Moments
{
    public class FriendRequestCell : ViewCell
    {
        public static readonly BindableProperty ConfirmCommandProperty =
            BindableProperty.Create(nameof(ConfirmCommand), typeof(ICommand), typeof(FriendRequestCell), null, propertyChanged: OnConfirmCommandPropertyChanged);

        public static readonly BindableProperty DenyCommandProperty =
            BindableProperty.Create(nameof(DenyCommand), typeof(ICommand), typeof(FriendRequestCell), null, propertyChanged: OnDenyCommandPropertyChanged);

        private static void OnConfirmCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = bindable as FriendRequestCell;
            cell.confirmCheckmarkButtonTapped.Command = (ICommand)newValue;
        }

        private static void OnDenyCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = bindable as FriendRequestCell;
            cell.denyCheckmarkButtonTapped.Command = (ICommand)newValue;
        }

        Label nameLabel;
        RoundedRectangleImage profilePhoto;
        Image confirmCheckmarkButton;
        Image denyCheckmarkButton;
        StackLayout leftLayout;
        StackLayout rightLayout;
        StackLayout mainLayout;

        TapGestureRecognizer confirmCheckmarkButtonTapped, denyCheckmarkButtonTapped;

        public FriendRequestCell()
        {
            SetupUserInterface();
            SetupEventHandlers();
            SetupBindings();
        }

        public ICommand ConfirmCommand
        {
            get => (ICommand)GetValue(ConfirmCommandProperty);
            set => SetValue(ConfirmCommandProperty, value);
        }

        public ICommand DenyCommand
        {
            get => (ICommand)GetValue(DenyCommandProperty);
            set => SetValue(DenyCommandProperty, value);
        }

        private void SetupUserInterface()
        {
            nameLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                FontAttributes = FontAttributes.None,
                FontFamily = "HelveticaNeue-Light",
                FontSize = 16,
                LineBreakMode = LineBreakMode.NoWrap,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };

            profilePhoto = new RoundedRectangleImage
            {
                HeightRequest = 55,
                WidthRequest = 55,
                VerticalOptions = LayoutOptions.Center
            };

            leftLayout = new StackLayout
            {
                Children = { profilePhoto, nameLabel },
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 0, 0, 0)
            };

            confirmCheckmarkButton = new Image
            {
                Source = Images.GreenCheckmark,
                HeightRequest = 43.75,
                WidthRequest = 33.75
            };

            denyCheckmarkButton = new Image
            {
                Source = "DeclineX.png",
                HeightRequest = 27.5,
                WidthRequest = 27.5
            };

            rightLayout = new StackLayout
            {
                Children = { confirmCheckmarkButton, denyCheckmarkButton },
                Padding = new Thickness(0, 0, 10, 0),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 25,
                Orientation = StackOrientation.Horizontal
            };

            mainLayout = new StackLayout
            {
                Children = { leftLayout, rightLayout },
                Orientation = StackOrientation.Horizontal
            };

            View = mainLayout;
        }

        private void SetupEventHandlers()
        {
            confirmCheckmarkButtonTapped = new TapGestureRecognizer();
            confirmCheckmarkButtonTapped.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(BindingContext), source: this));
            confirmCheckmarkButton.GestureRecognizers.Add(confirmCheckmarkButtonTapped);

            denyCheckmarkButtonTapped = new TapGestureRecognizer();
            denyCheckmarkButtonTapped.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(BindingContext), source: this));
            denyCheckmarkButton.GestureRecognizers.Add(denyCheckmarkButtonTapped);
        }

        private void SetupBindings()
        {
            nameLabel.SetBinding(Label.TextProperty, "Name");
            profilePhoto.SetBinding(Image.SourceProperty, "ProfileImage");
        }
    }
}
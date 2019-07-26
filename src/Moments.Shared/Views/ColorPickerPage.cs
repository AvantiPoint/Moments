using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Moments.Views
{
    public class ColorPickerPage : BasePage
    {
        Grid grid;

        //public ColorPickerPage(DrawMomentViewModel viewModel)
        public ColorPickerPage()
        {
            Title = Strings.PenColor;
            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            var stackLayout = new StackLayout();
            this.SizeChanged += (sender, e) => {
                stackLayout.Children.Clear();

                grid = BuildGrid();
                SetupEventHandlers();
                stackLayout.Children.Add(grid);
            };

            var mainLayout = new RelativeLayout();
            mainLayout.Children.Add(stackLayout,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = mainLayout;
        }

        private void SetupEventHandlers()
        {
            foreach (var child in grid.Children)
            {
                var button = child as Button;
                button.Clicked += NewColorSelection;
            }
        }

        private Grid BuildGrid()
        {
            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (Width / 3) },
                    new ColumnDefinition { Width = new GridLength (Width / 3) },
                    new ColumnDefinition { Width = new GridLength (Width / 3) }
                },
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (Height / 4) },
                    new RowDefinition { Height = new GridLength (Height / 4) },
                    new RowDefinition { Height = new GridLength (Height / 4) },
                    new RowDefinition { Height = new GridLength (Height / 4) },
                },
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorOne, BorderColor = Color.White }, 0, 0);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorTwo, BorderColor = Color.White }, 1, 0);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorThree, BorderColor = Color.White }, 2, 0);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorFour, BorderColor = Color.White }, 0, 1);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorFive, BorderColor = Color.White }, 1, 1);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorSix, BorderColor = Color.White }, 2, 1);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorSeven, BorderColor = Color.White }, 0, 2);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorEight, BorderColor = Color.White }, 1, 2);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorNine, BorderColor = Color.White }, 2, 2);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorTen, BorderColor = Color.White }, 0, 3);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorEleven, BorderColor = Color.White }, 1, 3);
            grid.Children.Add(new Button { BackgroundColor = Colors.ColorPickerColorTwelve, BorderColor = Color.White }, 2, 3);

            // TODO: Handle Pen Color Changes
            //var currentPenColor = grid.Children.Single(x => x.BackgroundColor == ViewModel.PenColor) as Button;
            //currentPenColor.BorderWidth = 5;

            return grid;
        }

        private void NewColorSelection(object s, EventArgs e)
        {
            foreach (var child in grid.Children)
            {
                var button = child as Button;
                button.BorderWidth = 0;
            }

            var selectedButton = s as Button;
            selectedButton.BorderWidth = 5;

            // TODO: Handle PenColor Changes
            //ViewModel.PenColor = selectedButton.BackgroundColor;
            Navigation.PopAsync();
        }
    }
}

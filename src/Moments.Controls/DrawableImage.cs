using Xamarin.Forms;

// Source: https://github.com/MitchMilam/Drawit
namespace Moments.Controls
{
    public class DrawableImage : Image
    {
        public static readonly BindableProperty CurrentLineColorProperty =
            BindableProperty.Create(nameof(CurrentLineColor), typeof(Color), typeof(DrawableImage), Color.Default);

        public Color CurrentLineColor
        {
            get => (Color)GetValue(CurrentLineColorProperty);
            set => SetValue(CurrentLineColorProperty, value);
        }
    }
}


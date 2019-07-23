using Xamarin.Forms;

// Source: https://github.com/MitchMilam/Drawit
namespace Moments
{
	public class DrawableImage : Image
	{
		public static readonly BindableProperty CurrentLineColorProperty = 
			BindableProperty.Create ((DrawableImage w) => w.CurrentLineColor, Color.Default);

		public Color CurrentLineColor 
		{
			get 
			{
				return (Color)GetValue (CurrentLineColorProperty);
			}
			set 
			{
				SetValue (CurrentLineColorProperty, value);
			}
		}
	}
}


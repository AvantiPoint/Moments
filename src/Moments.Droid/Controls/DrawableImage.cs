using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Moments.Controls;
using Android.Content;

// Source: https://github.com/MitchMilam/Drawit
[assembly: ExportRenderer(typeof(Moments.Controls.DrawableImage), typeof(Moments.Android.DrawableImageRenderer))]
namespace Moments.Android
{
	public class DrawableImageRenderer : ViewRenderer<DrawableImage, DrawView> 
	{
        public DrawableImageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DrawableImage> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				SetNativeControl(new DrawView(Context));
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == DrawableImage.CurrentLineColorProperty.PropertyName)
			{
				UpdateControl();
			}
		}

		private void UpdateControl()
		{
			Control.CurrentLineColor = Element.CurrentLineColor.ToAndroid();
		}
	}
}
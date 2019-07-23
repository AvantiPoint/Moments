using System;
using System.ComponentModel;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (Moments.RoundedRectangleImage), typeof (Moments.iOS.RoundedRectangleImageRenderer))]
namespace Moments.iOS
{
	public class RoundedRectangleImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
				return;

			try {
				Control.Layer.CornerRadius = 15f;
				Control.Layer.MasksToBounds = false;
				Control.ClipsToBounds = true;
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.WidthProperty.PropertyName) {

				try {
					Control.Layer.CornerRadius = 15f;
					Control.Layer.MasksToBounds = false;
					Control.ClipsToBounds = true;
				} catch (Exception ex) {
					Xamarin.Insights.Report (ex);
				}
			}
		}
	}
}
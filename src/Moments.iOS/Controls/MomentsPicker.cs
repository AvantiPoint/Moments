using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (Moments.MomentsPicker), typeof (Moments.iOS.MomentsPickerRenderer))]
namespace Moments.iOS
{
	public class MomentsPickerRenderer : PickerRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			try {
				Control.TextColor = UIColor.Clear;
				Control.Font = UIFont.FromName ("HelveticaNeue-Light", 16);
				Control.TextAlignment = UITextAlignment.Center;
				Control.BorderStyle = UITextBorderStyle.None;
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex);
			}
		}
	}
}
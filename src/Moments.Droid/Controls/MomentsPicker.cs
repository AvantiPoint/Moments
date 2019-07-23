using System;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof (Moments.MomentsPicker), typeof (Moments.Droid.MomentsPickerRenderer))]
namespace Moments.Droid
{
	public class MomentsPickerRenderer : PickerRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			try {
				Control.SetTextColor (global::Android.Content.Res.ColorStateList.ValueOf (global::Android.Graphics.Color.Transparent));
				Control.SetBackgroundColor (global::Android.Graphics.Color.Transparent);
			} catch (Exception ex) {
                Crashes.TrackError(ex);
			}
		}
	}
}
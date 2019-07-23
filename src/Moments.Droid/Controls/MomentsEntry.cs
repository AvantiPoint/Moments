using System;
using Android.Content.Res;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof (Moments.MomentsEntry), typeof (Moments.Droid.MomentsEntry))]
namespace Moments.Droid
{
	public class MomentsEntry : EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (Control != null) {
				Control.SetHintTextColor (ColorStateList.ValueOf (global::Android.Graphics.Color.White));
				Control.SetBackgroundDrawable (null);
			}
		}
	}
}

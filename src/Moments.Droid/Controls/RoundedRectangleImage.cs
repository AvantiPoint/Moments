using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using ImageCircle.Forms.Plugin.Droid;
using System;
using System.Diagnostics;
using System.ComponentModel;

// Source: https://github.com/jamesmontemagno/Xamarin.Plugins/blob/master/ImageCircle/ImageCircle.Forms.Plugin.Android/ImageCircleRenderer.cs
[assembly: ExportRenderer (typeof(Moments.RoundedRectangleImage), typeof(Moments.Droid.RoundedRectangleImageRenderer))]
namespace Moments.Droid
{
	[Preserve]
	public class RoundedRectangleImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				//Only enable hardware accelleration on lollipop
				if ((int)global::Android.OS.Build.VERSION.SdkInt < 21)
				{
					SetLayerType(LayerType.Software, null);
				}

			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == ImageCircle.Forms.Plugin.Abstractions.CircleImage.BorderColorProperty.PropertyName ||
				e.PropertyName == ImageCircle.Forms.Plugin.Abstractions.CircleImage.BorderThicknessProperty.PropertyName)
			{
				this.Invalidate();
			}
		}

		protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
		{
			try
			{
				var path = new Path ();
				path.AddRoundRect (new RectF (0, 0, Width, Height), 15f, 15f, Path.Direction.Ccw);
				canvas.Save ();
				canvas.ClipPath (path);

				var result = base.DrawChild(canvas, child, drawingTime);

				canvas.Restore();
				path.Dispose();
				return result;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to create rounded rectangle image: " + ex);
			}

			return base.DrawChild(canvas, child, drawingTime);
		}
	}
}
using System;
using System.IO;
using Android.App;

using Android.Graphics;

// Source: http://danielhindrikes.se/xamarin/building-a-screenshotmanager-to-capture-the-screen-with-code/
[assembly: Xamarin.Forms.Dependency (typeof (Moments.Droid.ScreenshotServiceAndroid))]
namespace Moments.Droid
{
	public class ScreenshotServiceAndroid : ScreenshotService
	{
		public static Activity Activity { get; set; }

		public byte[] CaptureScreen ()
		{
			var view = Activity.Window.DecorView;
			view.DrawingCacheEnabled = true;

			var bitmap = view.GetDrawingCache(true);

			byte[] bitmapData;
			using (var stream = new MemoryStream ())
			{
				bitmap.Compress (Bitmap.CompressFormat.Png, 0, stream);
				bitmapData = stream.ToArray ();
			}

			return bitmapData;
		}
	}
}
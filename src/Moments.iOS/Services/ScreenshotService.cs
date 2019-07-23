using System;
using CoreGraphics;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency (typeof (Moments.iOS.ScreenshotServiceiOS))]
namespace Moments.iOS
{
	public class ScreenshotServiceiOS : ScreenshotService
	{
		public ScreenshotServiceiOS ()
		{
			
		}

		public byte[] CaptureScreen ()
		{
			var screenshot = UIScreen.MainScreen.Capture ();

			return screenshot.AsJPEG ().ToArray ();
		}
	}
}


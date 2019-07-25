using System;
using System.IO;
using Android.App;

using Android.Graphics;
using Moments.Events;
using Prism.Events;

// Source: http://danielhindrikes.se/xamarin/building-a-screenshotmanager-to-capture-the-screen-with-code/
namespace Moments.Droid
{
    public class ScreenshotServiceAndroid : IScreenshotService
    {
        public static Activity Activity { get; set; }

        private IEventAggregator EventAggregator { get; }

        public ScreenshotServiceAndroid(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public byte[] CaptureScreen()
        {
            EventAggregator.GetEvent<HideButtonsEvent>().Publish();

            var view = Activity.Window.DecorView;
            view.DrawingCacheEnabled = true;

            var bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }
    }
}
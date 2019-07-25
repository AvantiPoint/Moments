using System;
using CoreGraphics;
using Foundation;
using Moments.Events;
using Prism.Events;
using UIKit;

namespace Moments.iOS
{
    public class ScreenshotServiceiOS : IScreenshotService
    {
        private IEventAggregator EventAggregator { get; }

        public ScreenshotServiceiOS(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public byte[] CaptureScreen()
        {
            EventAggregator.GetEvent<HideButtonsEvent>().Publish();

            var screenshot = UIScreen.MainScreen.Capture();

            return screenshot.AsJPEG().ToArray();
        }
    }
}


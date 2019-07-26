using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
    public partial class CameraPage
    {
        public CameraPage()
        {
            InitializeComponent();
        }

        public event EventHandler ImageCapturing;

        public event EventHandler<ImageCapturedEventArgs> ImageCaptured;

        public void SendImageCapturing() =>
            ImageCapturing?.Invoke(this, EventArgs.Empty);

        public void SendImageCaptured(byte[] imageBytes) =>
            ImageCaptured?.Invoke(this, new ImageCapturedEventArgs(imageBytes));
    }

    public class ImageCapturedEventArgs : EventArgs
    {
        public ImageCapturedEventArgs(byte[] imageBytes) => ImageBytes = imageBytes;

        public byte[] ImageBytes { get; }
    }
}


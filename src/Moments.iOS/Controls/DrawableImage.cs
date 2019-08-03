using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;
using Moments.Controls;

// Source: https://github.com/MitchMilam/Drawit
[assembly: ExportRenderer(typeof(Moments.Controls.DrawableImage), typeof(Moments.iOS.DrawableImageRenderer))]
namespace Moments.iOS
{
    public class DrawableImageRenderer : ViewRenderer<DrawableImage, DrawView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DrawableImage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                SetNativeControl(new DrawView(CGRect.Empty));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DrawableImage.CurrentLineColorProperty.PropertyName)
            {
                UpdateControl();
            }
        }

        private void UpdateControl()
        {
            Control.CurrentLineColor = Element.CurrentLineColor.ToUIColor();
        }
    }
}
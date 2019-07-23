using CoreAnimation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (Moments.MomentsEntry), typeof (Moments.iOS.MomentsEntryRenderer))]
namespace Moments.iOS
{
	public class MomentsEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (Control != null) {
				Control.BackgroundColor = UIColor.FromRGB (119, 171, 233);
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Font = UIFont.FromName ("HelveticaNeue-Thin", 20);
				Control.SetValueForKeyPath (UIColor.White, new NSString ("_placeholderLabel.textColor"));
				Control.Layer.SublayerTransform = CATransform3D.MakeTranslation (10, 0, 0);
			}
		}
	}
}
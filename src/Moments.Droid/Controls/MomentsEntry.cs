using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Moments.Controls.MomentsEntry), typeof(Moments.Droid.MomentsEntry))]
namespace Moments.Droid
{
    public class MomentsEntry : EntryRenderer
    {
        public MomentsEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
                Control.SetBackgroundDrawable(null);
            }
        }
    }
}

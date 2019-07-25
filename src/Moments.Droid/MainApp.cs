using System;

using Android.App;
using Android.Runtime;

using Xamarin.Forms;

namespace Moments.Droid
{
    [Application]
    public class MainApp : global::Android.App.Application
    {
        protected MainApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}

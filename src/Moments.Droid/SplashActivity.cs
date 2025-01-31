﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

// Source: https://developer.xamarin.com/guides/android/user_interface/creating_a_splash_screen/
namespace Moments.Droid
{
    [Activity(Label = "Moments", MainLauncher = true, Theme = "@style/Theme.NoTitleBar", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashLayout);

            StartActivity(typeof(MainActivity));
        }
    }
}
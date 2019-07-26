using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Moments.Views
{
    public partial class ViewMomentPage
    {
        //public ViewMomentPage (string image, TimeSpan viewMomentTime)
        public ViewMomentPage()
        {
            //BindingContext = new ViewMomentViewModel (image, viewMomentTime);

            InitializeComponent();
        }

        // TODO: Come back to this pop modal... determine the best approach later...
        //private void SetupUserInterface()
        //{
        //    momentImage.PropertyChanged += (sender, args) =>
        //    {
        //        var image = (Image)sender;

        //        if (args.PropertyName == "IsLoading" && !image.IsLoading)
        //        {
        //            Device.StartTimer(ViewModel.MomentViewTime, () => {
        //                Navigation.PopModalAsync();
        //                return false;
        //            });
        //        }
        //    };
        //}
    }
}


using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;

namespace Moments.Views
{
    public partial class SendMomentPage
    {
        //public SendMomentPage(Stream image, int displayTime)
        public SendMomentPage()
        {
            //BindingContext = new SendMomentViewModel(this, image, displayTime);

            InitializeComponent();
        }

        private void OnFriendsListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
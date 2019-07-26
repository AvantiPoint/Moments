using System;
using Xamarin.Forms;

namespace Moments.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            firstNameEntry.Completed += (object sender, EventArgs e) => {
                lastNameEntry.Focus();
            };

            lastNameEntry.Completed += (object sender, EventArgs e) => {
                usernameEntry.Focus();
            };

            usernameEntry.Completed += (object sender, EventArgs e) => {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) => {
                emailEntry.Focus();
            };
        }
    }
}


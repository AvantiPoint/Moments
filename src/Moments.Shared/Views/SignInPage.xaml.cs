namespace Moments.Views
{
    public partial class SignInPage
    {
        public SignInPage()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            usernameEntry.Completed += (sender, e) => {
                passwordEntry.Focus();
            };
        }
    }
}


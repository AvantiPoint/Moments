namespace Moments.Views
{
    public partial class MomentsPage
    {
        public MomentsPage()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            momentsListView.ItemSelected += (s, e) => {
                momentsListView.SelectedItem = null;
            };
        }
    }
}


using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Moments.Views
{
    public abstract class BasePage : ContentPage
    {
        public ObservableCollection<ToolbarItem> LeftToolbarItems { get; set; }

        public BasePage()
        {
            LeftToolbarItems = new ObservableCollection<ToolbarItem>();

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconImageSourceProperty, new Binding("Icon"));
            SetBinding(Page.IsBusyProperty, new Binding("IsBusy"));
        }
    }
}


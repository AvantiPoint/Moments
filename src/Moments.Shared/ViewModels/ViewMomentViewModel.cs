using System;
using Moments.Mvvm;
using Prism.AppModel;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI.Fody.Helpers;

namespace Moments.ViewModels
{
    public class ViewMomentViewModel : BaseViewModel, IAutoInitialize
    {
        public ViewMomentViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger) : base(navigationService, dialogService, logger)
        {
        }

        [AutoInitialize(true)]
        [Reactive]
        public string Image { get; set; }

        [AutoInitialize(true)]
        [Reactive]
        public TimeSpan MomentViewTime { get; set; }
    }
}
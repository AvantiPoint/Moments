using System;
using Prism.AppModel;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Moments.Dialogs
{
    public class LoadingViewModel : ReactiveObject, IDialogAware, IAutoInitialize
    {
        public event Action<IDialogParameters> RequestClose;

        private bool CanClose;

        [Reactive]public string Message { get; set; }

        public bool CanCloseDialog() => CanClose;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var isLoadingObservable = parameters.GetValue<IObservable<bool>>("isLoading");
            isLoadingObservable.Subscribe(isLoading =>
            {
                CanClose = !isLoading;
                if (CanClose)
                {
                    RequestClose(null);
                }
            });
        }
    }
}

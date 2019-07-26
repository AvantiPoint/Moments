using System;
using System.Reactive;
using Prism.AppModel;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Moments.Dialogs
{
    public class AlertDialogViewModel : ReactiveObject, IDialogAware, IAutoInitialize
    {
        public AlertDialogViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => RequestClose(null));
        }

        public event Action<IDialogParameters> RequestClose;

        [Reactive]public string Title { get; set; }

        [Reactive]public string Message { get; set; }

        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            CloseCommand.Dispose();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}

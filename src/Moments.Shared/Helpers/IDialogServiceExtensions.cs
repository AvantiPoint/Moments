using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using Acr.UserDialogs;
using Moments.Dialogs;
using Prism.Services.Dialogs;

namespace Moments.Helpers
{
    public static class IDialogServiceExtensions
    {
        static IDialogServiceExtensions()
        {
            IsLoading = new Subject<bool>();
            IsLoading.OnNext(false);
        }

        private static Subject<bool> IsLoading { get; }

        public static void ShowError(this IDialogService dialogService, string errorMessage)
        {
            // TODO: Implement a dialog
            dialogService.ShowDialog(nameof(AlertDialog), new DialogParameters { { "message", errorMessage }, { "title", "Error" } });
        }

        public static void ShowLoading(this IDialogService dialogService, string message)
        {
            // TODO: Implement a loading screen...
            IsLoading.OnNext(true);
            dialogService.ShowDialog(nameof(LoadingView), new DialogParameters { { "message", message } });
        }

        public static void HideLoading(this IDialogService dialogService)
        {
            IsLoading.OnNext(false);
        }

        public static void ShowSuccess(this IDialogService dialogService, string successMessage, int timeOut = 0)
        {
            UserDialogs.Instance.Toast(successMessage, TimeSpan.FromSeconds(timeOut));
        }
    }
}

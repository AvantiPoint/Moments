using System;
using System.Collections.Generic;
using System.Text;
using Prism.Services.Dialogs;

namespace Moments.Helpers
{
    public static class IDialogServiceExtensions
    {
        public static void ShowError(this IDialogService dialogService, string errorMessage)
        {
            // TODO: Implement a dialog
        }

        public static void ShowLoading(this IDialogService dialogService, string message)
        {
            // TODO: Implement a loading screen...
        }

        public static void HideLoading(this IDialogService dialogService)
        {

        }

        public static void ShowSuccess(this IDialogService dialogService, string successMessage, int timeOut = 0)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;

namespace Moments.Helpers
{
    public static class IUserDialogExtensions
    {
        public static void ShowError(this IUserDialogs instance, string message)
        {
            instance.Alert(message, "Error", "Ok");
        }
    }
}

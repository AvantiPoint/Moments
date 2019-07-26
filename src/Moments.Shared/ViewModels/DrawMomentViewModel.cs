using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Moments.Mvvm;
using ReactiveUI.Fody.Helpers;
using Prism.AppModel;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System.Reactive;
using ReactiveUI;

namespace Moments.ViewModels
{
    public class DrawMomentViewModel : BaseViewModel, IAutoInitialize
    {
        private IScreenshotService ScreenshotService { get; }

        public DrawMomentViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IScreenshotService screenshotService) : base(navigationService, dialogService, logger)
        {
            ScreenshotService = screenshotService;
            PenColor = Color.Yellow;
            SelectedIndex = 2;

            CancelCommand = ReactiveCommand.CreateFromTask(OnCancelCommandExecuted);
            ColorPickerCommand = ReactiveCommand.CreateFromTask(OnColorPickerCommandExecuted);
            SendCommand = ReactiveCommand.CreateFromTask(OnSendCommandExecuted);
        }

        [AutoInitialize("imageBytes", true)]
        [Reactive]
        public byte[] Image { get; set; }

        [Reactive]
        public Color PenColor { get; set; }

        [Reactive]
        public int SelectedIndex { get; set; }

        public int DisplayTime
        {
            get { return SelectedIndex + 3; }
        }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        private async Task OnCancelCommandExecuted() =>
            await NavigationService.GoBackAsync();

        public ReactiveCommand<Unit, Unit> ColorPickerCommand { get; }

        // TODO: Do we need to pass parameters???
        private async Task OnColorPickerCommandExecuted() =>
            await NavigationService.NavigateAsync("ColorPickerPage");

        public ReactiveCommand<Unit, Unit> SendCommand { get; }

        private async Task OnSendCommandExecuted()
        {
            var imageBytes = ScreenshotService.CaptureScreen();
            await NavigationService.NavigateAsync("SendMomentPage", new NavigationParameters
            {
                { "image", imageBytes },
                { "momentDisplayTime", DisplayTime }
            });
        }
    }
}

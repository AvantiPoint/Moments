using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Moments.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI;
using Moments.Helpers;
using System.Threading.Tasks;

namespace Moments.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        public CameraViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger) 
            : base(navigationService, dialogService, logger)
        {
            ImageCapturingCommand = ReactiveCommand.Create(OnImageCapturingCommandExecuted);
            ImageCapturedCommand = ReactiveCommand.CreateFromTask<byte[]>(OnImageCapturedCommandExecuted);
        }

        public ReactiveCommand<Unit, Unit> ImageCapturingCommand { get; }

        public ReactiveCommand<byte[], Unit> ImageCapturedCommand { get; }

        private void OnImageCapturingCommandExecuted()
        {
            DialogService.ShowLoading("Capturing Every Pixel");
        }

        private async Task OnImageCapturedCommandExecuted(byte[] imageBytes)
        {
            DialogService.HideLoading();
            await NavigationService.NavigateAsync("NavigationPage?useModalNavigation=true/DrawMomentPage", ("imageBytes", imageBytes));
        }
    }
}

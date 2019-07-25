using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Moments.Mvvm
{
    public abstract class BaseViewModel : ReactiveObject, IInitialize, IInitializeAsync, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IPageLifecycleAware
    {
        protected INavigationService NavigationService { get; }
        protected IDialogService DialogService { get; }
        protected ILogger Logger { get; }

        protected BaseViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            Logger = logger;
        }

        [Reactive]public string Title { get; set; }
        [Reactive]public string Subtitle { get; set; }
        [Reactive]public string Icon { get; set; }
        [Reactive]public bool IsBusy { get; set; }

        protected virtual void Initialize(INavigationParameters parameters) { }

        void IInitialize.Initialize(INavigationParameters parameters) => Initialize(parameters);

        protected virtual Task InitializeAsync(INavigationParameters paramters) => Task.CompletedTask;

        Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

        protected virtual void Destroy() { }
        void IDestructible.Destroy() => Destroy();

        protected virtual bool CanNavigate(INavigationParameters parameters) => true;

        bool IConfirmNavigation.CanNavigate(INavigationParameters parameters) => CanNavigate(parameters);

        protected virtual Task<bool> CanNavigateAsync(INavigationParameters parameters) => Task.FromResult(true);
        Task<bool> IConfirmNavigationAsync.CanNavigateAsync(INavigationParameters parameters) => CanNavigateAsync(parameters);

        protected virtual void OnAppearing() { }
        void IPageLifecycleAware.OnAppearing() => OnAppearing();

        protected virtual void OnDisappearing() { }
        void IPageLifecycleAware.OnDisappearing() => OnDisappearing();
    }
}

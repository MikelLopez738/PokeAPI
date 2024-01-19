using CommunityToolkit.Mvvm.ComponentModel;
using System;
using WPF.Application.Core;

namespace WPF.Application.Services
{
    public interface INavigationService
    {
        public BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
    }

    public partial class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        [ObservableProperty]
        public BaseViewModel _currentView;

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TBaseViewModel>() where TBaseViewModel : BaseViewModel
        {
           BaseViewModel baseViewModel = _viewModelFactory.Invoke(typeof(TBaseViewModel));
            CurrentView = baseViewModel;
        }
    }
}

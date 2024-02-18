using CommunityToolkit.Mvvm.ComponentModel;
using System;
using WPF.Application.Core;
using WPF.Application.MVVM.Model.Interfaces;
using WPF.Application.MVVM.ViewModel;

namespace WPF.Application.Services
{
    public interface INavigationService
    {
        public BaseViewModel CurrentView { get; }
        void NavigateTo<T>(object parameter = null) where T : BaseViewModel, new();
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

        public void NavigateTo<TBaseViewModel>(object parameter = null) where TBaseViewModel : BaseViewModel, new()
        {
            BaseViewModel baseViewModel = _viewModelFactory.Invoke(typeof(TBaseViewModel));
            baseViewModel.SetParameters(parameter);
            CurrentView = baseViewModel;
        }
    }
}

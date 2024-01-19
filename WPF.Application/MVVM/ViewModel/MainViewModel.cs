﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF.Application.Core;
using WPF.Application.Services;

namespace WPF.Application.MVVM.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        public INavigationService _navigation;


        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
        }

        [RelayCommand]
        public void NavigateHome()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }

        [RelayCommand]
        public void NavigateSettings()
        {
            Navigation.NavigateTo<SettingsViewModel>();
        }
    }
}
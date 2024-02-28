using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WPF.Application.Core;
using WPF.Application.Model;
using WPF.Application.MVVM.Model.Interfaces;
using WPF.Application.Services;

namespace WPF.Application.MVVM.ViewModel
{
    public partial class ItemsViewModel : BaseViewModel
    {
        private readonly IPokemonDataService PokemonDataService;

        [ObservableProperty]
        public INavigationService _navigation;

        [ObservableProperty]
        Visibility _mostrarLoading = Visibility.Collapsed;

        [ObservableProperty]
        public List<RootItem> items;

        public ItemsViewModel()
        {
        }

        public ItemsViewModel(IPokemonDataService pokemonDataService, INavigationService navigation)
        {
            Items = new();
            PokemonDataService = pokemonDataService;
            _navigation = navigation;
            GetItems();
        }

        [RelayCommand]
        public async void GetItems()
        {
            Items.Clear();
            MostrarLoading = Visibility.Visible;
            Items = await PokemonDataService.GetItems("", ElementosPaginado);
            MostrarLoading = Visibility.Collapsed;
        }

        [RelayCommand]
        public async Task Anterior()
        {
            Items.Clear();
            MostrarLoading = Visibility.Visible;
            Items = await PokemonDataService.GetItems("N", ElementosPaginado);
            MostrarLoading = Visibility.Collapsed;
        }

        [RelayCommand]
        public async Task Siguiente()
        {
            Items.Clear();
            MostrarLoading = Visibility.Visible;
            Items = await PokemonDataService.GetItems("S", ElementosPaginado);
            MostrarLoading = Visibility.Collapsed;
        }
    }
}

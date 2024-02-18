using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF.Application.Core;
using WPF.Application.Model;
using WPF.Application.MVVM.Model.Interfaces;
using WPF.Application.Services;

namespace WPF.Application.MVVM.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IPokemonDataService PokemonDataService;

        [ObservableProperty]
        public INavigationService _navigation;

        [ObservableProperty]
        public List<Pokemon> pokemons;

        [ObservableProperty]
        public Pokemon _currentPokemon;

        [ObservableProperty]
        Visibility _mostrarLoading = Visibility.Collapsed;

        public BitmapImage imagen { get; set; }

        public HomeViewModel()
        {
        }

        public HomeViewModel(IPokemonDataService pokemonDataService, INavigationService navigation)
        {
            Pokemons = new();
            PokemonDataService = pokemonDataService;
            GetPokemons();
            _navigation = navigation;
        }

        [RelayCommand]
        public async void GetPokemons()
        {
            MostrarLoading = Visibility.Visible;
            Pokemons = await PokemonDataService.GetPokemons();
            MostrarLoading = Visibility.Collapsed;
        }

        [RelayCommand]
        public void NavegarDetalle(object name)
        {
            Navigation.NavigateTo<DetallePokemonViewModel>(name);
        }
    }
}

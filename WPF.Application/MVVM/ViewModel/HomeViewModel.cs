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
        public int _generacionSeleccionada;

        [ObservableProperty]
        Visibility _mostrarLoading = Visibility.Collapsed;

        public BitmapImage imagen { get; set; }

        [ObservableProperty]
        public bool _comboBoxEnable;

        public HomeViewModel()
        {
        }
         
        public HomeViewModel(IPokemonDataService pokemonDataService, INavigationService navigation)
        {
            Pokemons = new();
            PokemonDataService = pokemonDataService;
            _navigation = navigation;
            GeneracionSeleccionada = 0;
            GetPokemons();
        }

        [RelayCommand]
        public async void GetPokemons()
        {
            Pokemons.Clear();
            ComboBoxEnable = false;
            MostrarLoading = Visibility.Visible;
            Pokemons = await PokemonDataService.GetPokemons(GeneracionSeleccionada);
            MostrarLoading = Visibility.Collapsed;
            ComboBoxEnable = true;
        }

        [RelayCommand]
        public void NavegarDetalle(object name)
        {
            Navigation.NavigateTo<DetallePokemonViewModel>(name);
        }

        [RelayCommand]
        public void GeneracionCambiada(object obj)
        {
            var a = obj;
        }

        partial void OnGeneracionSeleccionadaChanged(int value)
        {
            GeneracionSeleccionada = value;
            GetPokemons();
        }
    }
}

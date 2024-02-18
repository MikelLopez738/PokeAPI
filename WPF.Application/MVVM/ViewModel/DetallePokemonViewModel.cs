using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF.Application.Core;
using WPF.Application.Model;
using WPF.Application.MVVM.Model.Interfaces;

namespace WPF.Application.MVVM.ViewModel
{
    public partial class DetallePokemonViewModel : BaseViewModel
    {
        private IPokemonDataService PokemonDataService;

        [ObservableProperty]
        public string _nombrePokemonSeleccionado;

        [ObservableProperty]
        public string _sprite;

        [ObservableProperty]
        public Pokemon pokemon;

        public DetallePokemonViewModel(IPokemonDataService pokemonDataService)
        {
            PokemonDataService = pokemonDataService;
            //Consulta para traer los datos del pokemon seleccionado.
        }

        public DetallePokemonViewModel()
        {
                
        }

        [RelayCommand]
        public async void CargarDatos()
        {
            NombrePokemonSeleccionado = NombrePokemon;
            if (NombrePokemonSeleccionado != null)
            {
                Pokemon = await PokemonDataService.GetPokemon(NombrePokemonSeleccionado);
                var a = Pokemon;
            }
        }
    }
}

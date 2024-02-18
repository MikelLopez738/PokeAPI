using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPF.Application.Model;

namespace WPF.Application.MVVM.Model.Interfaces
{
    public interface IPokemonDataService
    {
        public Task<List<Pokemon>> GetPokemons();
        public Task<Pokemon> GetPokemon(string nombrePokemon);
    }
}

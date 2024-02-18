using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPF.Application.Model;
using WPF.Application.MVVM.Model.Interfaces;

namespace WPF.Application.Services
{
    public class PokemonDataService : IPokemonDataService
    {
        public async Task<List<Pokemon>> GetPokemons() //Recogemos los datos de los pokemon para mostrar en el listado.
        {
            using (HttpClient httpClient = new HttpClient())
            {
                List<PokemonBase> pokemonsBase = new();
                List<Pokemon> infoPokemons = new();
                try
                {
                    string apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=151&offset=0";
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Root? rootPokemon = JsonConvert.DeserializeObject<Root>(responseBody);

                        if (rootPokemon != null)
                        {
                            pokemonsBase.AddRange(rootPokemon.results.Select(item => new PokemonBase
                            {
                                Nombre = item.name,
                                url = item.url
                            }));
                        }
                    }

                    if (pokemonsBase.Count > 0)
                    {
                        foreach (var pokemon in pokemonsBase)
                        {
                            HttpResponseMessage result = await httpClient.GetAsync(pokemon.url);
                            if (result.IsSuccessStatusCode)
                            {
                                // Leer y procesar la respuesta
                                string responseBody = await result.Content.ReadAsStringAsync();
                                RootPokemon? rootPokemon = JsonConvert.DeserializeObject<RootPokemon>(responseBody);

                                infoPokemons.Add(new Pokemon()
                                {
                                    IndexPokedex = rootPokemon.id,
                                    Name = rootPokemon.name,
                                    Sprite = rootPokemon.sprites.front_default,
                                });
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

                return infoPokemons;
            }
        }

        public async Task<Pokemon> GetPokemon(string nombrePokemon) //Recogemos los datos del pokemon seleccionado.
        {
            Pokemon infoPokemon = new();
            string urlAPI = "https://pokeapi.co/api/v2/pokemon/" + nombrePokemon;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage result = await httpClient.GetAsync(urlAPI);
                if (result.IsSuccessStatusCode)
                {
                    // Leer y procesar la respuesta
                    string responseBody = await result.Content.ReadAsStringAsync();
                    RootPokemon? rootPokemon = JsonConvert.DeserializeObject<RootPokemon>(responseBody);

                    List<string> moves = new();
                    List<string> types = new();
                    List<statsPokemon> stats = new();

                    moves = rootPokemon.abilities.Select(item => item.ability.name).ToList();
                    moves = rootPokemon.types.Select(item => item.type.name).ToList();
                    stats = rootPokemon.stats.Select(item => new statsPokemon
                    {
                        nameStat = item.stat.name,
                        amount = item.base_stat
                    }).ToList();


                    infoPokemon.IndexPokedex = rootPokemon.id;
                    infoPokemon.Weight = rootPokemon.weight.ToString();
                    infoPokemon.Sprite = rootPokemon.sprites.front_default;
                    infoPokemon.official_artwork = rootPokemon.sprites.other.officialartwork.front_default.ToString();
                    infoPokemon.imageShowdown = rootPokemon.sprites.other.showdown.front_default.ToString();
                    infoPokemon.Name = rootPokemon.name;
                    infoPokemon.Height = rootPokemon.height.ToString();
                    infoPokemon.moves = moves;
                    infoPokemon.TypeName = types;
                    infoPokemon.stats = stats;
                }

            }

            return infoPokemon;
        }
    }
}

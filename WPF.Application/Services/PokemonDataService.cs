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
        public async Task<List<Pokemon>> GetPokemons(int generacion = 0) //Recogemos los datos de los pokemon para mostrar en el listado.
        {
            string apiUrl = "";
            switch (generacion)
            {
                case 0:
                    apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=151&offset=0";
                    break;
                case 1:
                    apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=100&offset=151";
                    break;
                case 2:
                    apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=135&offset=251";
                    break;
                case 3:
                    apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=107&offset=386";
                    break;
                default:
                    apiUrl = "https://pokeapi.co/api/v2/pokemon?limit=151&offset=0";
                    break;
            }
            

            using (HttpClient httpClient = new HttpClient())
            {
                List<PokemonBase> pokemonsBase = new();
                List<Pokemon> infoPokemons = new();
                try
                {
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
                                    imageShowdown = rootPokemon.sprites.other.showdown.front_default
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
            Pokemon pokemonSiguiente = new();
            Pokemon pokemonAnterior = new();
            RootPokemon? rootPokemon = new();
            List<string> moves = new();
            List<TypePokemon> types = new();
            List<statsPokemon> stats = new();
            List<MovimientosPokemon> movimientosPokemons = new();

            string urlAPI = "https://pokeapi.co/api/v2/pokemon/" + nombrePokemon;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage result = await httpClient.GetAsync(urlAPI);
                if (result.IsSuccessStatusCode)
                {
                    // Leer y procesar la respuesta
                    string responseBody = await result.Content.ReadAsStringAsync();
                    rootPokemon = JsonConvert.DeserializeObject<RootPokemon>(responseBody);

                    moves = rootPokemon.moves.Select(item => item.move.name).ToList();

                    foreach (var item in rootPokemon.types)
                    {
                        types.Add(new TypePokemon()
                        {
                            name = item.type.name,
                            image = "/Assets/Images/" + item.type.name + ".png",
                        });
                    }

                    stats = rootPokemon.stats.Select(item => new statsPokemon
                    {
                        nameStat = item.stat.name,
                        amount = item.base_stat
                    }).ToList();
                }

                //Anterior y siguiiente ppokemon
                int idAnteriorPokemon = rootPokemon.id - 1;
                int idSiiguientePokemon = rootPokemon.id + 1;
                if (idAnteriorPokemon < 1)
                {
                    idAnteriorPokemon = 493;
                }
                if (idSiiguientePokemon > 493)
                {
                    idSiiguientePokemon = 1;
                }

                //Anterior
                urlAPI = "https://pokeapi.co/api/v2/pokemon/" + idAnteriorPokemon;
                HttpResponseMessage pokemonAnteriorResult = await httpClient.GetAsync(urlAPI);
                if (pokemonAnteriorResult.IsSuccessStatusCode)
                {
                    // Leer y procesar la respuesta
                    string responseBody = await pokemonAnteriorResult.Content.ReadAsStringAsync();
                    var anteriorPokemon = JsonConvert.DeserializeObject<RootPokemon>(responseBody);

                    infoPokemon.PokemonAnterior = new();
                    infoPokemon.PokemonAnterior.IndexPokedex = anteriorPokemon.id;
                    infoPokemon.PokemonAnterior.Name = anteriorPokemon.name;
                }

                //Siguiente
                urlAPI = "https://pokeapi.co/api/v2/pokemon/" + idSiiguientePokemon;
                HttpResponseMessage pokemonSiguienteResult = await httpClient.GetAsync(urlAPI);
                if (pokemonSiguienteResult.IsSuccessStatusCode)
                {
                    // Leer y procesar la respuesta
                    string responseBody = await pokemonSiguienteResult.Content.ReadAsStringAsync();
                    var siguientePokemon = JsonConvert.DeserializeObject<RootPokemon>(responseBody);

                    infoPokemon.PokemonSiguiente = new();
                    infoPokemon.PokemonSiguiente.IndexPokedex = siguientePokemon.id;
                    infoPokemon.PokemonSiguiente.Name = siguientePokemon.name;
                }
            }

            //Detalles pokemon actual.
            infoPokemon.IndexPokedex = rootPokemon.id;
            infoPokemon.Weight = rootPokemon.weight.ToString();
            infoPokemon.Sprite = rootPokemon.sprites.front_default;
            infoPokemon.SpriteBack = rootPokemon.sprites.back_default;
            infoPokemon.SpriteShiny = rootPokemon.sprites.front_shiny;
            infoPokemon.SpriteShinyBack = rootPokemon.sprites.back_shiny;
            infoPokemon.official_artwork = rootPokemon.sprites.other.officialartwork.front_default.ToString();
            infoPokemon.imageShowdown = rootPokemon.sprites.other.showdown.front_default.ToString();
            infoPokemon.Name = rootPokemon.name;
            infoPokemon.Height = rootPokemon.height.ToString();
            infoPokemon.moves = moves;
            infoPokemon.Types = types;
            infoPokemon.stats = stats;

            return infoPokemon;
        }

        public async Task<List<RootItem>> GetItems(string suma, int paginado)
        {
            int limit = 30;
            switch (suma)
            {
                case "S":
                    paginado += 30;
                    if (paginado >= 2110)
                    {
                        paginado = 2110 - 60;
                    }
                    break;
                case "N":
                    paginado -= 30;
                    if (paginado < 0)
                    {
                        paginado = 0;
                    }
                    break;
                default:
                    paginado = 0;
                    break;
            }

            string apiUrl = "https://pokeapi.co/api/v2/item/?offset="+ paginado +"&limit=" + limit;
            using (HttpClient httpClient = new HttpClient())
            {
                List<ItemBase> itemBase = new();
                List<RootItem> infoItem = new();
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        ItemBase? rootItems = JsonConvert.DeserializeObject<ItemBase>(responseBody);

                        if (rootItems != null)
                        {
                            itemBase.Add(new ItemBase()
                            {
                                next = rootItems.next,
                                previous = rootItems.previous,
                                count = rootItems.count,
                                results = rootItems.results,
                            });
                        }
                    }

                    if (itemBase.Count > 0)
                    {
                        foreach (var item in itemBase.Select(i => i.results).First())
                        {
                            HttpResponseMessage result = await httpClient.GetAsync(item.url);
                            if (result.IsSuccessStatusCode)
                            {
                                // Leer y procesar la respuesta
                                string responseBody = await result.Content.ReadAsStringAsync();
                                RootItem? rootItem = JsonConvert.DeserializeObject<RootItem>(responseBody);

                                infoItem.Add(new RootItem()
                                {
                                    name = item.name,
                                    sprite = rootItem.sprites.@default.ToString(),
                                }) ;
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

                return infoItem;
            }
        }
    }
}

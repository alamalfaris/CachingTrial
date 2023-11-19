using InMemoryCaching.Models;

namespace InMemoryCaching.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<Pokemons>> GetPokemons();
        Task CreatePokemons(Pokemons pokemons);
        Task<Pokemons> GetPokemon(int pokemonId);

    }
}

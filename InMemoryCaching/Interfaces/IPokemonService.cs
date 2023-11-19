using InMemoryCaching.Models;

namespace InMemoryCaching.Interfaces
{
    public interface IPokemonService
    {
		Task<List<Pokemons>> GetCachedPokemons(string cacheKey, SemaphoreSlim semaphore);
		Task CreatePokemons(Pokemons pokemons, string cacheKey);
        Task<Pokemons> GetPokemon(int pokemonId, SemaphoreSlim semaphore);

    }
}

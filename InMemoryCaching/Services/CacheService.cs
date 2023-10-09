using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Services
{
    public class CacheService : ICacheService
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly IPokemonRepository _pokemonRepository;

        public CacheService(IMemoryCache cache, IPokemonRepository pokemonRepository)
        {
            _cache = cache;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<List<Pokemons>> GetCachedPokemons(string cacheKey, SemaphoreSlim semaphore)
        {
            bool isAvailable = _cache.TryGetValue(cacheKey, out List<Pokemons>? pokemons);
            if (isAvailable && pokemons is not null)
            {
                return pokemons;
            }
            else
            {
                return pokemons;
            }
        }
    }
}

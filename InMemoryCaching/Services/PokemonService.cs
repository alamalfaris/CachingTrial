using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMemoryCache _cache;
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IMemoryCache cache, IPokemonRepository pokemonRepository)
        {
            _cache = cache;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<Pokemons> GetPokemon(int pokemonId, SemaphoreSlim semaphore)
        {
            bool isAvailable = _cache.TryGetValue(pokemonId, out Pokemons? pokemon);
            if (isAvailable && pokemon is not null)
            {
                return pokemon;
            }

            try
            {
                await semaphore.WaitAsync();

                isAvailable = _cache.TryGetValue(pokemonId, out pokemon);
                if (isAvailable && pokemon is not null)
                {
                    return pokemon;
                }

                pokemon = await _pokemonRepository.GetPokemon(pokemonId);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };
                _cache.Set(pokemonId, pokemon, cacheEntryOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                semaphore.Release();
            }
            return pokemon is not null ? pokemon : new Pokemons();
        }

        public async Task<List<Pokemons>> GetCachedPokemons(string cacheKey, SemaphoreSlim semaphore)
        {
            bool isAvailable = _cache.TryGetValue(cacheKey, out List<Pokemons>? pokemons);
            if (isAvailable && pokemons is not null)
            {
                return pokemons;
            }
            
            try
            {
				await semaphore.WaitAsync();

                isAvailable = _cache.TryGetValue(cacheKey, out pokemons);
				if (isAvailable && pokemons is not null)
				{
					return pokemons;
				}

                pokemons = await _pokemonRepository.GetPokemons();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };
                _cache.Set(cacheKey, pokemons, cacheEntryOptions);
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                semaphore.Release();
            }
            return pokemons is not null ? pokemons : new List<Pokemons>();
        }

        public async Task CreatePokemons(Pokemons pokemons, string cacheKey)
        {
            try
            {
                await _pokemonRepository.CreatePokemons(pokemons);
                _cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

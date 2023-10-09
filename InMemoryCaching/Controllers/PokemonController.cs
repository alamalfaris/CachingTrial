using InMemoryCaching.Helpers;
using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMemoryCache _cache;

        public PokemonController(IPokemonRepository pokemonRepository,
            IMemoryCache cache)
        {
            _pokemonRepository = pokemonRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult> GetPokemon()
        {
            if (!_cache.TryGetValue(CacheKeys.Pokemons, out List<Pokemons>? pokemons))
            {
                pokemons = await _pokemonRepository.GetPokemons();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _cache.Set(CacheKeys.Pokemons, pokemons, cacheEntryOptions);
            }
            return Ok();
        }
    }
}

using InMemoryCaching.Helpers;
using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
		private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
		private readonly IPokemonService _pokemonService;

		public PokemonController(IPokemonService pokemonService)
        {
			_pokemonService = pokemonService;
		}

        [HttpGet("{pokemonId}")]
        public async Task<ActionResult> GetPokemons(int pokemonId)
        {
            try
            {
                var pokemons = await _pokemonService.GetPokemon(pokemonId, _semaphoreSlim);
                return Ok(pokemons);
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ex.Message,
                    ContentType = "application/json"
                };
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPokemons()
        {
            try
            {
                var pokemons = await _pokemonService.GetCachedPokemons(CacheKeys.Pokemons, _semaphoreSlim);
                return Ok(pokemons);
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ex.Message,
                    ContentType = "application/json"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePokemons([FromBody] Pokemons pokemons)
        {
            try
            {
                await _pokemonService.CreatePokemons(pokemons, CacheKeys.Pokemons);
                return Ok(pokemons);
            }
            catch (Exception ex)
            {
				return new ContentResult()
				{
					StatusCode = 500,
					Content = ex.Message,
					ContentType = "application/json"
				};
			}
        }
    }
}

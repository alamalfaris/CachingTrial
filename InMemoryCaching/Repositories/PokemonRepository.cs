using InMemoryCaching.Database;
using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCaching.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DatabaseContext _context;

        public PokemonRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Pokemons> GetPokemon(int pokemonId)
        {
            return await _context.Pokemons.FirstAsync(m => m.Id == pokemonId);
        }

        public async Task<List<Pokemons>> GetPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task CreatePokemons(Pokemons pokemons)
        {
            _context.Pokemons.Add(pokemons);
            await _context.SaveChangesAsync();
        }
    }
}

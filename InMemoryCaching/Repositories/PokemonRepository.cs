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

        public async Task<List<Pokemons>> GetPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }
    }
}

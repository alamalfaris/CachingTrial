using InMemoryCaching.Models;

namespace InMemoryCaching.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<Pokemons>> GetPokemons();
    }
}

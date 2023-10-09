using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCaching.Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

        public virtual DbSet<Pokemons> Pokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Pokemons>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Pokemons");

                entity.ToTable("Pokemons");
            });
        }
	}
}

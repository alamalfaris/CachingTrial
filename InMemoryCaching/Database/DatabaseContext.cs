using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCaching.Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

		public virtual DbSet<Employee> Employee { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasKey(e => e.Id)
					.HasName("PK__employee__3213E83FCC17573B");

				entity.ToTable("employee");
			});
		}
	}
}

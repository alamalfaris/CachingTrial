using InMemoryCaching.Database;
using InMemoryCaching.Interfaces;
using InMemoryCaching.Repositories;
using InMemoryCaching.Services;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCaching
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

            // DbContext
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
			builder.Services.AddScoped<IPokemonService, PokemonService>();

			// Memory Cache
			builder.Services.AddMemoryCache();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
using GoldenRaspberryAwards.API.Application.Movies;
using GoldenRaspberryAwards.API.Configuration;
using GoldenRaspberryAwards.Application.Awards;
using GoldenRaspberryAwards.Application.DataSeed;
using GoldenRaspberryAwards.Application.Movies;
using GoldenRaspberryAwards.Infrastructure.Contexts;
using GoldenRaspberryAwards.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GoldenRaspberryAwards.API.Extensions;

public static class ProgramExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(SwaggerConfiguration.ConfigureSwaggerGen);

        var root = new InMemoryDatabaseRoot();

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("GoldenRaspberryAwardsDb", root)
        );

        services.AddTransient<IAwardService, AwardService>();
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<IMovieRepository, MovieRepository>();
        services.AddScoped<DataSeederService>();


        return services;
    }



    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<DataSeederService>();
            seeder.Seed(@"DataSeed\movieslist.csv");

        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}

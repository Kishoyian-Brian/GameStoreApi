using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGame(this WebApplicationBuilder builder)
    {
        var conString = "Host=localhost;Port=5432;Database=GamestoreDb;User Id=postgres;Password=#####";
        builder.Services.AddDbContext<GameStoreContext>(options =>
            options.UseNpgsql(conString)
                .UseSeeding((context, _) =>
                {
                    if (!context.Set<Genre>().Any())
                    {
                        context.Set<Genre>().AddRange(
                            new Genre { Name = "Fighting" },
                            new Genre { Name = "RPG" },
                            new Genre { Name = "Taken" },
                            new Genre { Name = "Zombie @2" },
                            new Genre { Name = "Shooting" }
                        );
                        context.SaveChanges();
                    }
                }));
    }
}

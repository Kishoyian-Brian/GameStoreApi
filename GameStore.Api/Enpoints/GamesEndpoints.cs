using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Enpoints;

public static class GamesEndpoints
{
    private const string EndpointName = "GetGame";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .AsNoTracking()
                .Include(game => game.Genre)
                .Select(game => new GameSummaryDto(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleseaseDate))
                .ToListAsync());

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games
                .AsNoTracking()
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

            return game is null
                ? Results.NotFound()
                : Results.Ok(new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleseaseDate));
        }).WithName(EndpointName);

        group.MapPost("/", async (CreateGmaeDto newGame, GameStoreContext dbContext) =>
        {
            if (!await dbContext.Genres.AnyAsync(g => g.Id == newGame.GenreId))
            {
                return Results.BadRequest(new { error = $"Genre {newGame.GenreId} not found." });
            }

            var game = new Game
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleseaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            var gameDetailsDto = new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleseaseDate);

            return Results.CreatedAtRoute(EndpointName, new { id = gameDetailsDto.Id }, gameDetailsDto);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

          

            existingGame.Name = updateGameDto.Name;
            existingGame.GenreId = updateGameDto.GenreId;
            existingGame.Price = updateGameDto.Price;
            existingGame.ReleseaseDate = updateGameDto.ReleaseDate;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game is null)
            {
                return Results.NotFound();
            }

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}

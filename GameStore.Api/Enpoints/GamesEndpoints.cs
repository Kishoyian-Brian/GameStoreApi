using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Enpoints
{
    public static class GamesEndpoints
    {
        const string EndpointName = "GetGame";
        private static readonly List<GamesDto> games = [
       new(
        1,
        "StreetFight",
        "Fighting",
        19.99M,
        new DateOnly(2002,6,8)
    ),
    new(
        2,
        "Final Destination",
        "Horror",
        30.98M,
        new DateOnly(2001,4,12)
    ),
    new(
        3,
        "GTA VI",
        "Crime Thriller",
        200.5M,
        new DateOnly(2023,5,26)
    )
   ];

        public static void MapGamesEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("/games");

            //Get /games
            group.MapGet("/games", () => games);

            // Get /games/1
            group.MapGet("/games/{id}", (int id) =>
            {
                var game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            }).WithName(EndpointName);


            //POST /games
            group.MapPost("/", (CreateGmaeDto newGame, GameStoreContext dbContext) =>
            {
               Game game = new()
               {
                   Name=newGame.Name,
                   GenreId = newGame.GenreId,
                   Price= newGame.Price,
                   ReleseaseDate=newGame.ReleaseDate
               };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                GameDetailsDto gameDetailsDto=new(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleseaseDate
                );

                return Results.CreatedAtRoute(EndpointName, new { id = gameDetailsDto.Id }, gameDetailsDto);
            });


            //PUT /games/1
            group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                games[index] = new GamesDto(
                    id,
                    updateGameDto.Name,
                    updateGameDto.Genre,
                    updateGameDto.Price,
                    updateGameDto.ReleaseDate
                );
                return Results.NoContent();
            });

            //DELET /games/id
            app.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            });
        }

    }
}
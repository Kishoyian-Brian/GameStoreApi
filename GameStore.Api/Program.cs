using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GamesDto> games = [
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

//Get /games

app.MapGet("/games", () => games);

const string EndpointName = "GetGame";

// Get /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(EndpointName);

//POST /games
app.MapPost("/games", (CreateGmaeDto newGame) =>
{
    GamesDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
});

app.Run();

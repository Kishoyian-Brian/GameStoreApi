using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Enpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
var conString = "Host=localhost;Port=5432;Database=GamestoreDb;User Id=postgres;Password=brian123";


builder.Services.AddNpgsql<GameStoreContext>(conString);


var app = builder.Build();
app.MapGamesEndpoints();

app.MigrateDb();

app.Run();

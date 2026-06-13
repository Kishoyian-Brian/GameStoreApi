using GameStore.Api.Data;
using GameStore.Api.Enpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.AddGame();

var app = builder.Build();
app.MapGamesEndpoints();
app.MigrateDb();

app.Run();

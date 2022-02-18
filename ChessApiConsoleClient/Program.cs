using ChessApiClient;
using ChessApiConsoleClient.Commands;
using Cocona;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var appSettings = new ConfigurationBuilder().
    AddJsonFile("appsettings.json")
    .Build();

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<IChessApiClient>(i =>
    ActivatorUtilities.CreateInstance<ChessApiClient.ChessApiClient>(i, appSettings["ChessApi:BaseAddress"])
);

using var app = builder.Build();

app.MapEndPoints();
app.Run();
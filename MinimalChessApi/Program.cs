using MinimalChessApi.API;
using MinimalChessApi.Controller;

const string GameStateDirectory = "_games";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IChessController>(i => 
    ActivatorUtilities.CreateInstance<ChessController>(i, GameStateDirectory)
);

var app = builder.Build();
app.MapEndPoints();
app.Run();

using MinimalChessApi.API;
using MinimalChessApi.Controller;
using MinimalChessApi.Services;

const string GameStateDirectory = "_games";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IChessController, ChessController>();
builder.Services.AddSingleton<IGameStoreService>(i => 
    ActivatorUtilities.CreateInstance<FileStoreService>(i, GameStateDirectory)
);

var app = builder.Build();
app.MapEndPoints();
app.Run();

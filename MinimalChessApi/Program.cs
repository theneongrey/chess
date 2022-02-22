using MinimalChessApi.API;
using MinimalChessApi.Controller;
using MinimalChessApi.Services;

const string GameStateDirectory = "_games";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IChessController, ChessController>();
builder.Services.AddSingleton<IGameStoreService>(i => 
    ActivatorUtilities.CreateInstance<FileStoreService>(i, GameStateDirectory)
);

#region Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();
#endregion

app.MapEndPoints();
app.Run();

using Microsoft.AspNetCore.Mvc;
using MinimalChessApi.Controller;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IChessController>(i => ActivatorUtilities.CreateInstance<ChessController>(i, "games"));

var app = builder.Build();

app.MapGet("/", () => "Hello Chess!");
app.MapPost("/game", ([FromServices] IChessController chessHandler) =>
{
    try
    {
        var game = chessHandler.NewGame();
        return Results.Created($"/game/{game.GameId}", game);
    }
    catch
    {
        return Results.Problem();
    }

});

app.MapGet("/game", ([FromServices] IChessController chessHandler) =>
{
    var games = chessHandler.GetGameReferences();
    return games == null ? Results.NotFound() : Results.Ok(games);
});

app.MapGet("/game/{id}", ([FromServices] IChessController chessHandler, string id) =>
{
    var game = chessHandler.GetGame(id);
    return game == null ? Results.NotFound() : Results.Ok(game);
});

app.MapPut("/game/move/{id}/{from}/{to}", async ([FromServices] IChessController chessHandler, string id, string from, string to) =>
{
    if (await chessHandler.MovePiece(id, from, to))
    {
        return Results.Ok();
    }
    else
    {
        return Results.Problem();
    }
});

app.Run();

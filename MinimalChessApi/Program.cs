using MinimalChessApi;

var builder = WebApplication.CreateBuilder(args);
await using var app = builder.Build();
var chessHandling = new ChessHandling("games");

app.MapGet("/", () => "Hello World!");
app.MapGet("/new-game", () => chessHandling.NewGame());
app.MapGet("/get-game/{id}", (string id) => chessHandling.GetBoard(id));

await app.RunAsync();

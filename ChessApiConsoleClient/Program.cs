using ChessApiConsoleClient.Commands;
using Cocona;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.MapEndPoints();
app.Run();
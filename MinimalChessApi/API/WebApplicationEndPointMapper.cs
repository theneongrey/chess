using MinimalChessApi.Controller;

namespace MinimalChessApi.API
{
    public static class WebApplicationEndPointMapper
    {
        private static IResult NewGame(IChessController chessController)
        {
            try
            {
                var game = chessController.NewGame();
                return Results.Created($"/game/{game.GameId}", game);
            }
            catch
            {
                return Results.Problem();
            }
        }

        private static IResult GetGameReferences(IChessController chessController)
        {
            var games = chessController.GetGameReferences();
            return games == null ? Results.Problem() : Results.Ok(games);
        }

        private static IResult GetGameById(IChessController chessController, string id)
        {
            var game = chessController.GetGame(id);
            return game == null ? Results.NotFound() : Results.Ok(game);
        }

        private static IResult GetAllowedMoves(IChessController chessController, string id, string pieceCellName)
        {
            var moves = chessController.GetAllowedMoves(id, pieceCellName);
            return moves == null ? Results.Problem() : Results.Ok(moves);
        }

        private static async Task<IResult> MovePiece(IChessController chessController, string id, string fromCellName, string toCellName)
        {
            if (await chessController.MovePiece(id, fromCellName, toCellName))
            {
                return Results.Ok();
            }
            else
            {
                return Results.Problem();
            }
        }

        public static void MapEndPoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello Chess!");
            app.MapPost("/game", NewGame);
            app.MapGet("/game", GetGameReferences);
            app.MapGet("/game/{id}", GetGameById);
            app.MapGet("/game/allowed-moves/{id}/{pieceCellName}", GetAllowedMoves);
            app.MapPut("/game/move/{id}/{fromCellName}/{toCellName}", MovePiece);
        }
    }
}

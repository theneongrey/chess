using ChessApiContract;
using MinimalChessApi.Controller;

namespace MinimalChessApi.API
{
    public static class WebApplicationEndPointMapper
    {
        private static IResult NewGame(IChessController chessController)
        {
            var result = chessController.NewGame();
            if (result.WasSuccessful)
            {
                return Results.Created($"/game/{result.GameId}", result);
            }
            return Results.Problem(result.Error);
        }

        private static IResult GetGameReferences(IChessController chessController)
        {
            var result = chessController.GetGameList();
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static IResult GetGameById(IChessController chessController, string id)
        {
            var result = chessController.GetGame(new Guid(id));
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static IResult GetAllowedMoves(IChessController chessController, string id, string pieceCellName)
        {
            var result = chessController.GetAllowedMoves(new Guid(id), pieceCellName);
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static async Task<IResult> MovePiece(IChessController chessController, string id, string fromCellName, string toCellName)
        {
            var result = await chessController.MovePiece(new Guid(id), fromCellName, toCellName);
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        public static void MapEndPoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello Chess!");
            app.MapPost($"/{Calls.NewGame}", NewGame);
            app.MapGet($"/{Calls.GameList}", GetGameReferences);
            app.MapGet($"/{Calls.GameById}/{{id}}", GetGameById);
            app.MapGet($"/{Calls.AllowedMoves}/{{id}}/{{pieceCellName}}", GetAllowedMoves);
            app.MapPut($"/{Calls.MovePiece}/{{id}}/{{fromCellName}}/{{toCellName}}", MovePiece);
        }
    }
}

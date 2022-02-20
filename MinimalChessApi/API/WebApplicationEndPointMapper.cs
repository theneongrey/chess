using ChessApiContract;
using ChessApiContract.Request;
using Microsoft.AspNetCore.Mvc;
using MinimalChessApi.Controller;

namespace MinimalChessApi.API
{
    public static class WebApplicationEndPointMapper
    {
        private static async Task<IResult> NewGameAsync(IChessController chessController)
        {
            var result = await chessController.NewGameAsync();
            if (result.WasSuccessful)
            {
                return Results.Created($"/game/{result.GameId}", result);
            }
            return Results.Problem(result.Error);
        }

        private static async Task<IResult> GetGameReferencesAsync(IChessController chessController)
        {
            var result = await chessController.GetGameListAsync();
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static async Task<IResult> GetGameByIdAsync(IChessController chessController, string id)
        {
            var result = await chessController.GetGameAsync(new Guid(id));
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static async Task<IResult> GetAllowedMovesAsync(IChessController chessController, string id, string pieceCellName)
        {
            var result = await chessController.GetAllowedMovesAsync(new Guid(id), pieceCellName);
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        private static async Task<IResult> MovePieceAsync(IChessController chessController, string id, [FromBody] MoveRequest move)
        {
            var result = await chessController.MovePieceAsync(new Guid(id), move.FromCellName, move.ToCellName);
            if (result.WasSuccessful)
            {
                return Results.Ok(result);
            }
            return Results.Problem(result.Error);
        }

        public static void MapEndPoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello Chess!");
            app.MapPost($"/{Calls.NewGame}", NewGameAsync);
            app.MapGet($"/{Calls.GameList}", GetGameReferencesAsync);
            app.MapGet($"/{Calls.GameById}/{{id}}", GetGameByIdAsync);
            app.MapGet($"/{Calls.AllowedMoves}/{{id}}/{{pieceCellName}}", GetAllowedMovesAsync);
            app.MapPut($"/{Calls.MovePiece}/{{id}}", MovePieceAsync);
        }
    }
}

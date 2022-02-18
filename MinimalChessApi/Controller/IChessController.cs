using ChessApiContract.Response;

namespace MinimalChessApi.Controller
{
    public interface IChessController
    {
        NewGameResponse NewGame();
        GameListResponse GetGameList();
        GetGameResponse GetGame(Guid gameId);
        Task<MovePieceResponse> MovePiece(Guid gameId, string fromCellName, string toCellName);
        AllowedMovesResponse GetAllowedMoves(Guid id, string pieceCellName);
    }
}

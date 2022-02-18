using MinimalChessApi.Model;

namespace MinimalChessApi.Controller
{
    public interface IChessController
    {
        GameReferenceModel NewGame();
        IEnumerable<GameReferenceModel> GetGameReferences();
        GameModel? GetGame(Guid gameId);
        Task<bool> MovePiece(Guid gameId, string fromCellName, string toCellName);
        AllowedMoves? GetAllowedMoves(Guid id, string pieceCellName);
    }
}

using MinimalChessApi.Model;

namespace MinimalChessApi.Controller
{
    public interface IChessController
    {
        GameReferenceModel NewGame();
        IEnumerable<GameReferenceModel> GetGameReferences();
        GameModel? GetGame(string gameId);
        Task<bool> MovePiece(string gameId, string fromCellName, string toCellName);
        AllowedMoves? GetAllowedMoves(string id, string pieceCellName);
    }
}

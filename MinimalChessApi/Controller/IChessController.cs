using MinimalChessApi.Results;

namespace MinimalChessApi.Controller
{
    public interface IChessController
    {
        GameReferenceModel NewGame();
        IEnumerable<GameReferenceModel> GetGameReferences();
        GameModel? GetGame(string gameId);
        Task<bool> MovePiece(string gameId, string from, string to);

    }
}

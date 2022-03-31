using ChessApi.Model;

namespace ChessApi.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetGames();
        Game NewGame(Guid player1, Guid player2);
        Game? GetGameById(Guid id);
        Game? MovePiece(Guid id, CellPosition from, CellPosition to);
        bool DeleteGame(Guid id);
    }
}

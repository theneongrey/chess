using ChessApi.Dto;
using ChessApi.Model;

namespace ChessApi.Data
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetGames();
        Game? GetGameById(Guid id);
        Game NewGame(User player1, User player2);
        Game? UpdateGame(Guid id, string newState);
        bool DeleteGame(Guid id);
    }
}

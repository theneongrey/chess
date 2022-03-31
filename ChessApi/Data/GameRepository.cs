using ChessApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ChessApi.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _dataContext;

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Game> GetGames()
        {
            var games = _dataContext.Games!;
            return games.Include(game => game.Player1)
                        .Include(game => game.Player2); 
        }

        public Game? GetGameById(Guid id)
        {
            return GetGames().FirstOrDefault(x => x.Id == id);
        }

        public Game NewGame(User player1, User player2)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Player1 = player1,
                Player2 = player2,
                FullAllGebraicNotationState = string.Empty
            };

            _dataContext.Games!.Add(game);
            _dataContext.SaveChanges();

            return game;
        }

        public Game? UpdateGame(Guid id, string newState)
        {
            var game = GetGameById(id);
            if (game == null)
            {
                return null;
            }

            game.FullAllGebraicNotationState = newState;
            _dataContext.Update(game);
            _dataContext.SaveChanges();

            return game;
        }

        public bool DeleteGame(Guid id)
        {
            var game = GetGameById(id);
            if (game == null)
            {
                return false;
            }

            _dataContext.Games!.Remove(game);
            _dataContext.SaveChanges();
            return true;
        }
    }
}

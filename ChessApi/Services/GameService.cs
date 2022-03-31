using ChessApi.Data;
using ChessApi.Model;
using GameLogic;
using GameParser;

namespace ChessApi.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUsersRepository _usersRepository;

        public GameService(IGameRepository gameRepository, IUsersRepository usersRepository)
        {
            _gameRepository = gameRepository;
            _usersRepository = usersRepository;
        }

        public IEnumerable<Model.Game> GetGames()
        {
            return _gameRepository.GetGames();
        }

        public Model.Game NewGame(Guid player1, Guid player2)
        {
            var user1 = _usersRepository.GetUserById(player1);
            var user2 = _usersRepository.GetUserById(player2);

            return _gameRepository.NewGame(user1!, user2!);
        }

        public Model.Game? GetGameById(Guid id)
        {
            return _gameRepository.GetGameById(id);
        }

        public Model.Game? MovePiece(Guid id, CellPosition from, CellPosition to)
        {
            var game = _gameRepository.GetGameById(id);
            if (game == null)
            {
                return null;
            }

            var gameLogic = FullAlgebraicNotationParser.GetGameFromNotation(game.FullAllGebraicNotationState!);
            var piece = gameLogic.SelectPiece(new Position(from.PosX, from.PosY));
            if (piece == null)
            {
                throw new IllegalMoveException($"Can not select piece at {from}");
            }

            if (!gameLogic.TryMove(new Position(to.PosX, to.PosY)))
            {
                throw new IllegalMoveException($"Can not move piece from {from} to {to}");
            }

            var updatedGame = _gameRepository.UpdateGame(id, gameLogic.ToFullAlgebraicNotation());
            return updatedGame;
        }

        public bool DeleteGame(Guid id)
        {
            return _gameRepository.DeleteGame(id);
        }
    }
}

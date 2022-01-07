using GameLogic;
using GameParser;
using MinimalChessApi.Results;

namespace MinimalChessApi
{
    // todo: 
    // [ ] error handling
    // [ ] include swagger
    // [ ] move results to contract project
    // [ ] continue to implement logic

    public class ChessHandling
    {
        private const string GameExtension = "game";
        private string _targetPath;

        public ChessHandling(string targetPath)
        {
            _targetPath = targetPath;
            
            Clean();
        }

        private string GetGameFilename(string gameId)
        {
            return Path.Combine(_targetPath, $"{gameId}.{GameExtension}");
        }

        private Position? PositionFromName(string position)
        {
            if (position.Length < 2)
            {
                return null;
            }

            var x = position[0] - 'a';
            var y = position[1] - '1';

            try
            {
                return new Position(x, y);
            }
            catch
            {
                return null;
            }
        }

        public void Clean()
        {
            if (Directory.Exists(_targetPath))
            {
                Directory.Delete(_targetPath, true);
            }

            Directory.CreateDirectory(_targetPath);
        }

        public IEnumerable<string> GetGames()
        {
            if (Directory.Exists(_targetPath))
            {
                return Directory.GetFiles(_targetPath, $"*.{GameExtension}");
            }

            return Enumerable.Empty<string>();
        }

        public NewGameResult NewGame()
        {
            var id = Guid.NewGuid().ToString();
            var filename = GetGameFilename(id);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using var _ = File.Create(GetGameFilename(id));

            return new NewGameResult(id);
        }

        public BoardResult? GetBoard(string gameId)
        {
            var game = GetGame(gameId);
            if (game == null)
            {
                return null;
            }

            var cells = new List<string>();
            var pieces = game.GetBoard();
            foreach (var pieceRow in pieces)
            {
                foreach (var piece in pieceRow)
                {
                    cells.Append(piece?.Identifier ?? string.Empty);

                } 
            }

            return new BoardResult(cells);
        }

        private Game? GetGame(string gameId)
        {
            var filename = GetGameFilename(gameId);
            if (File.Exists(filename))
            {
                try
                {
                    var game = FullAlgebraicNotationParser.GetGameFromNotation(File.ReadAllText(filename));
                    return game;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public bool MovePiece(string gameId, string from, string to)
        {
            var game = GetGame(gameId);
            if (game == null)
            {
                return false;
            }
            
            var fromPosition = PositionFromName(from);
            var toPosition = PositionFromName(to);

            if (fromPosition == null || toPosition == null)
            {
                return false;
            }

            if (game.SelectPiece(fromPosition.Value) == null)
            {
                return false;
            }

            if (game.TryMove(toPosition.Value))
            {
                return true;
            }

            return false;
        }
    }
}

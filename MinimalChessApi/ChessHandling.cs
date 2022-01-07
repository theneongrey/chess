using GameLogic;
using GameParser;
using MinimalChessApi.Results;

namespace MinimalChessApi
{
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

            var x = char.ToLower(position[0]) - 'a';
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

        public IEnumerable<GameReferenceModel> GetGameReferences()
        {
            if (Directory.Exists(_targetPath))
            {
                return Directory.GetFiles(_targetPath, $"*.{GameExtension}")
                    .Select(f => new GameReferenceModel(Path.GetFileNameWithoutExtension(f)));
            }

            return Enumerable.Empty<GameReferenceModel>();
        }

        public GameReferenceModel NewGame()
        {
            var id = Guid.NewGuid().ToString();
            var filename = GetGameFilename(id);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using var _ = File.Create(GetGameFilename(id));

            return new GameReferenceModel(id);
        }

        public GameModel? GetBoard(string gameId)
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
                    cells.Add(piece?.Identifier ?? string.Empty);
                } 
            }

            var state = game.IsGameOver ? "Over" :
                        game.IsGameRunning ? "Running" :
                        game.IsPromotionPending ? "Promotion" :
                        "Unknown";

            return new GameModel(cells, state, true, game.IsCheckPending);
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

        public async Task<bool> MovePiece(string gameId, string from, string to)
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
            await File.WriteAllTextAsync(GetGameFilename(gameId), game.ToFullAlgebraicNotation());

            return false;
        }
    }
}

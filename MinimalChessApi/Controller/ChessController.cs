using GameLogic;
using GameParser;
using MinimalChessApi.Model;

namespace MinimalChessApi.Controller
{
    public class ChessController : IChessController
    {
        private const string GameExtension = "game";
        private string _targetPath;

        public ChessController(string targetPath)
        {
            _targetPath = targetPath;

            Clean();
        }

        private string GetGameFilename(Guid gameId)
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

        private void Clean()
        {
            if (Directory.Exists(_targetPath))
            {
                Directory.Delete(_targetPath, true);
            }

            Directory.CreateDirectory(_targetPath);
        }

        private Game? GetGameFromFile(Guid gameId)
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

        public GameReferenceModel NewGame()
        {
            var id = Guid.NewGuid();
            var filename = GetGameFilename(id);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using var _ = File.Create(GetGameFilename(id));

            return new GameReferenceModel(id);
        }

        public IEnumerable<GameReferenceModel> GetGameReferences()
        {
            if (Directory.Exists(_targetPath))
            {
                return Directory.GetFiles(_targetPath, $"*.{GameExtension}")
                    .Select(f =>
                    {
                        var fileName = Path.GetFileNameWithoutExtension(f);
                        if (Guid.TryParse(fileName, out var guid))
                        {
                            return new GameReferenceModel(guid);
                        }
                        return null;
                    }
                    )
                    .Where(f => f is not null)!;
            }

            return Enumerable.Empty<GameReferenceModel>();
        }

        public GameModel? GetGame(Guid gameId)
        {
            var game = GetGameFromFile(gameId);
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

            return new GameModel(cells, state, game.IsItWhitesTurn, game.IsCheckPending);
        }

        public async Task<bool> MovePiece(Guid gameId, string fromCellName, string toCellName)
        {
            var game = GetGameFromFile(gameId);
            if (game == null)
            {
                return false;
            }

            var fromPosition = PositionFromName(fromCellName);
            var toPosition = PositionFromName(toCellName);

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
                await File.WriteAllTextAsync(GetGameFilename(gameId), game.ToFullAlgebraicNotation());
                return true;
            }

            return false;
        }

        public AllowedMoves? GetAllowedMoves(Guid gameId, string pieceCellName)
        {
            var game = GetGameFromFile(gameId);
            if (game == null)
            {
                return null;
            }

            var piecePosition = PositionFromName(pieceCellName);
            if (piecePosition == null)
            {
                return null;
            }

            var moves = game.GetMovesForCell(piecePosition.Value);
            return new AllowedMoves(moves.Select(p => p.AsCellName()));
        }
    }
}

using ChessApiContract.Response;
using GameLogic;
using GameParser;

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

        public NewGameResponse NewGame()
        {
            var id = Guid.NewGuid();
            var filename = GetGameFilename(id);

            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                using var _ = File.Create(GetGameFilename(id));
                return NewGameResponse.RespondSuccess(id);
            }
            catch
            {
                return NewGameResponse.RespondError("Could not create a new game");
            }
        }

        public GameListResponse GetGameList()
        {
            if (Directory.Exists(_targetPath))
            {
                var result = new List<string>();
                foreach (var file in Directory.GetFiles(_targetPath, $"*.{GameExtension}"))
                {
                    var guidCandidate = Path.GetFileNameWithoutExtension(file);
                    if (Guid.TryParse(guidCandidate, out var _))
                    {
                        result.Add(guidCandidate);
                    }
                }

                return GameListResponse.RespondSuccess(result);
            }

            return GameListResponse.RespondError("Could not load games");
        }

        public GetGameResponse GetGame(Guid gameId)
        {
            var game = GetGameFromFile(gameId);
            if (game == null)
            {
                return GetGameResponse.RespondError("Game could not be loaded");
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

            return GetGameResponse.RespondSuccess(cells, state, game.IsItWhitesTurn, game.IsCheckPending);
        }

        public async Task<MovePieceResponse> MovePiece(Guid gameId, string fromCellName, string toCellName)
        {
            var game = GetGameFromFile(gameId);
            if (game == null)
            {
                return MovePieceResponse.RespondError("Game could not be loaded");
            }

            var fromPosition = PositionFromName(fromCellName);
            var toPosition = PositionFromName(toCellName);

            if (fromPosition == null)
            {
                return MovePieceResponse.RespondError("\"From\" position could not be interpreted");
            }
            if (toPosition == null)
            {
                return MovePieceResponse.RespondError("\"To\" position could not be interpreted");
            }

            if (game.SelectPiece(fromPosition.Value) == null)
            {
                return MovePieceResponse.RespondError("There is no valid piece at \"from\" position"); ;
            }

            if (game.TryMove(toPosition.Value))
            {
                try
                {
                    await File.WriteAllTextAsync(GetGameFilename(gameId), game.ToFullAlgebraicNotation());
                    return MovePieceResponse.RespondSuccess();
                }
                catch
                {
                    return MovePieceResponse.RespondError("Could not update game");
                }
            }

            return MovePieceResponse.RespondError("Illegal move"); ;
        }

        public AllowedMovesResponse GetAllowedMoves(Guid gameId, string pieceCellName)
        {
            var game = GetGameFromFile(gameId);
            if (game == null)
            {
                return AllowedMovesResponse.RespondError("Game could not be loaded");
            }

            var piecePosition = PositionFromName(pieceCellName);
            if (piecePosition == null)
            {
                return AllowedMovesResponse.RespondError("Given position could not be interpreted");
            }

            if (game.SelectPiece(piecePosition.Value) == null)
            {
                return AllowedMovesResponse.RespondError("There is no valid piece at given position");
            }

            var moves = game.GetMovesForCell(piecePosition.Value);
            return AllowedMovesResponse.RespondSuccess(moves.Select(p => p.AsCellName()));
        }
    }
}

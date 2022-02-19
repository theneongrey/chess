using ChessApiContract.Response;
using GameLogic;
using GameParser;
using MinimalChessApi.Services;

namespace MinimalChessApi.Controller
{
    public class ChessController : IChessController
    {
        private IGameStoreService _gameStore;

        public ChessController(IGameStoreService gameStore)
        {
            _gameStore = gameStore;
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

        private async Task<Game?> GetGameFromIdAsync(Guid gameId)
        {
            var gameContent = await _gameStore.LoadGameAsync(gameId);
            if (gameContent is not null)
            {
                return FullAlgebraicNotationParser.GetGameFromNotation(gameContent);
            }

            return null;
        }

        public async Task<NewGameResponse> NewGameAsync()
        {
            var id = Guid.NewGuid();
            if (await _gameStore.SaveGameAsync(id, string.Empty))
            {
                return NewGameResponse.RespondSuccess(id);
            }
            else
            {
                return NewGameResponse.RespondError("Could not create a new game");
            }
        }

        public async Task<GameListResponse> GetGameListAsync()
        {
            var games = await _gameStore.GetGamesAsync();
            if (games is not null)
            {
                return GameListResponse.RespondSuccess(games);
            }

            return GameListResponse.RespondError("Could not load games");
        }

        public async Task<GetGameResponse> GetGameAsync(Guid gameId)
        {
            var game = await GetGameFromIdAsync(gameId);
            if (game is null)
            {
                return GetGameResponse.RespondError($"Game \"{gameId}\" could not be loaded");
            }

            var cells = new List<string>();
            var pieces = game.GetBoard();
            foreach (var pieceRow in pieces)
            {
                foreach (var piece in pieceRow)
                {
                    cells.Add(piece?.ColoredIdentifier ?? string.Empty);
                }
            }

            var state = game.IsGameOver ? "Over" :
                        game.IsGameRunning ? "Running" :
                        game.IsPromotionPending ? "Promotion" :
                        "Unknown";

            return GetGameResponse.RespondSuccess(cells, state, game.IsItWhitesTurn, game.IsCheckPending);
        }

        public async Task<MovePieceResponse> MovePieceAsync(Guid gameId, string fromCellName, string toCellName)
        {
            var game = await GetGameFromIdAsync(gameId);
            if (game is null)
            {
                return MovePieceResponse.RespondError($"Game \"{gameId}\" could not be loaded");
            }

            var fromPosition = PositionFromName(fromCellName);
            var toPosition = PositionFromName(toCellName);

            if (fromPosition is null)
            {
                return MovePieceResponse.RespondError($"\"From\" position \"{fromPosition}\" could not be interpreted");
            }
            if (toPosition is null)
            {
                return MovePieceResponse.RespondError($"\"To\" \"{toPosition}\" position could not be interpreted");
            }

            if (game.SelectPiece(fromPosition.Value) is null)
            {
                return MovePieceResponse.RespondError($"There is no valid piece at \"from\" position \"{fromPosition}\""); ;
            }

            if (game.TryMove(toPosition.Value))
            {
                if (await _gameStore.SaveGameAsync(gameId, game.ToFullAlgebraicNotation()))
                {
                    return MovePieceResponse.RespondSuccess();
                }
                
                return MovePieceResponse.RespondError("Could not update game");
            }

            return MovePieceResponse.RespondError("Illegal move"); ;
        }

        public async Task<AllowedMovesResponse> GetAllowedMovesAsync(Guid gameId, string pieceCellName)
        {
            var game = await GetGameFromIdAsync(gameId);
            if (game is null)
            {
                return AllowedMovesResponse.RespondError($"Game \"{gameId}\" could not be loaded");
            }

            var piecePosition = PositionFromName(pieceCellName);
            if (piecePosition is null)
            {
                return AllowedMovesResponse.RespondError($"Given position \"{pieceCellName}\" could not be interpreted");
            }

            if (game.SelectPiece(piecePosition.Value) is null)
            {
                return AllowedMovesResponse.RespondError($"There is no valid piece at given position \"{piecePosition}\"");
            }

            var moves = game.GetMovesForCell(piecePosition.Value);
            return AllowedMovesResponse.RespondSuccess(moves.Select(p => p.AsCellName()));
        }
    }
}

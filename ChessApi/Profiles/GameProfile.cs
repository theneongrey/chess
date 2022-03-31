using AutoMapper;
using ChessApi.Dto;
using GameLogic;
using GameParser;

namespace ChessApi.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Model.Game, GameDto>().ConstructUsing((source, context) =>
            {
                var gameData = FullAlgebraicNotationParser.GetGameFromNotation(source.FullAllGebraicNotationState!);
                var cells = GetCellsFromBoard(gameData.GetBoard());
                var state = GetStateFromGame(gameData);
                var player1 = context.Mapper.Map<UserDto>(source.Player1);
                var player2 = context.Mapper.Map<UserDto>(source.Player2);

                return new GameDto(source.Id, cells, state,
                    gameData.IsItWhitesTurn, gameData.IsCheckPending,
                    player1, player2);
            });
            CreateMap<Model.Game, GameShortDto>().ConstructUsing((source, context) =>
            {
                var gameData = FullAlgebraicNotationParser.GetGameFromNotation(source.FullAllGebraicNotationState!);
                var state = GetStateFromGame(gameData);
                var player1 = context.Mapper.Map<UserDto>(source.Player1);
                var player2 = context.Mapper.Map<UserDto>(source.Player2);
                return new GameShortDto(source.Id, state, player1, player2);
            });
        }

        private string[] GetCellsFromBoard(GamePiece?[][] pieces)
        {
            var cells = new string[64];
            var index = 0;
            foreach (var pieceRow in pieces)
            {
                foreach (var piece in pieceRow)
                {
                    cells[index++] = piece?.ColoredIdentifier ?? string.Empty;
                }
            }

            return cells;
        }

        private string GetStateFromGame(GameLogic.Game game)
        {
            return game.IsGameOver ? "Over" :
                   game.IsGameRunning ? "Running" :
                   game.IsPromotionPending ? "Promotion" :
                   "Unknown";
        }
    }
}

using FluentAssertions;
using Xunit;

namespace GameLogic.Test.GameTest
{
    public class GameSimpleOneActionTest
    {
        [Fact]
        public void EnPassantForWhite_CapturesPiece()
        {
            const string expectedResultBoard = @"R-BQKBNR
PPPPP-PP
N----p--
--------
--------
--------
pppppp-p
rnbqkbnr";

            var game = new Game();

            // move white pawn
            game.SelectPiece(new Position(6, 1)).Should().Be(GamePieces.WhitePawn);
            game.TryMove(new Position(6, 3)).Should().BeTrue();

            // move any other black piece
            game.SelectPiece(new Position(1, 7)).Should().Be(GamePieces.BlackKnight);
            game.TryMove(new Position(0, 5)).Should().BeTrue();

            // move white pawn forward
            game.SelectPiece(new Position(6, 3)).Should().Be(GamePieces.WhitePawn);
            game.TryMove(new Position(6, 4)).Should().BeTrue();

            // move to be captured pawn
            game.SelectPiece(new Position(5, 6)).Should().Be(GamePieces.BlackPawn);
            game.TryMove(new Position(5, 4)).Should().BeTrue();

            // perform en passant
            game.SelectPiece(new Position(6, 4)).Should().Be(GamePieces.WhitePawn);
            game.GetMovesForCell(new Position(6, 4)).Should().Contain(new Position(5, 5));
            game.TryMove(new Position(5, 5)).Should().BeTrue();
            game.GetPieceAtCell(new Position(5, 5)).Should().Be(GamePieces.WhitePawn);
            game.GetPieceAtCell(new Position(5, 4)).Should().BeNull();

            game.ToString().Should().Be(expectedResultBoard);
        }
    }
}

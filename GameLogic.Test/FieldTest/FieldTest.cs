using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.FieldTest
{
    public class FieldTest
    {
        [Fact]
        public void SingleMoveTest()
        {
            var expectedField = @"RNBQKBNR
PPPPPPPP
--------
--------
-p------
--------
p-pppppp
rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);

            var from = new Position(1, 1);
            var to = new Position(1, 3);

            var pawnPiece = field.GetPieceAt(from);
            pawnPiece.Should().BeOfType<PawnPiece>();
            
            field.MovePiece(pawnPiece!, to);
            field.LastMovedPiece.Should().Be(pawnPiece);
            pawnPiece!.Position.Should().Be(to);
            pawnPiece!.LastPosition.Should().Be(from);

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }
    }
}

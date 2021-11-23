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
            
            field.MovePiece(from, to).Should().BeTrue();
            field.GetLastMovedPiece().Should().Be(pawnPiece);
            pawnPiece.Position.Should().Be(to);
            pawnPiece.LastPosition.Should().Be(from);

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }

        [Fact]
        public void CaptureTest()
        {
            var startField = @"RNBQKBNR
PP-PPPPP
--------
--P-----
-p------
--------
p-pppppp
rnbqkbnr";

            var expectedField = @"RNBQKBNR
PP-PPPPP
--------
--p-----
--------
--------
p-pppppp
rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(startField);

            var from = new Position(1, 3);
            var to = new Position(2, 4);

            var pawnPiece = field.GetPieceAt(from);
            pawnPiece.Should().BeOfType<PawnPiece>();

            field.MovePiece(from, to).Should().BeTrue();
            field.GetLastMovedPiece().Should().Be(pawnPiece);
            pawnPiece.Position.Should().Be(to);
            pawnPiece.LastPosition.Should().Be(from);

            var newPieceAtTo = field.GetPieceAt(to);
            newPieceAtTo.Should().BeOfType<PawnPiece>();
            newPieceAtTo.Color.Should().Be(PieceColor.White);

            field.GetPieceAt(from).Should().BeNull();

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }

    }
}

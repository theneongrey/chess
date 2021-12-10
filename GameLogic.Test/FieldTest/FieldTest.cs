using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.FieldTest
{
    public class FieldTest
    {
        [Fact]
        public void SingleAddTest()
        {
            var expectedField = @"----K---
--------
--------
--------
--------
--------
--------
----k---";
            var field = new Field();
            field.AddPiece(new KingPiece(new Position(4, 0), PieceColor.White));
            field.AddPiece(new KingPiece(new Position(4, 7), PieceColor.Black));

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }

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
            
            field.MovePieceTo(pawnPiece!, to);
            field.LastMovedPiece.Should().Be(pawnPiece);
            pawnPiece!.Position.Should().Be(to);

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }

        [Fact]
        public void SingleRemoveTest()
        {
            var expectedField = @"RNBQKBNR
PPPPPPPP
--------
--------
--------
--------
p-pppppp
rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);

            var cell = new Position(1, 1);

            var pawnPiece = field.GetPieceAt(cell);
            pawnPiece.Should().BeOfType<PawnPiece>();

            field.RemovePiece(pawnPiece!);
            
            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(expectedField);
        }
    }
}

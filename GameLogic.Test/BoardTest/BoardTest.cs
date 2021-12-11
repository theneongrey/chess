using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.BoardTest
{
    public class BoardTest
    {
        [Fact]
        public void SingleAddTest()
        {
            var expectedBoard = @"----K---
--------
--------
--------
--------
--------
--------
----k---";
            var board = new Board();
            board.AddPiece(new KingPiece(new Position(4, 0), PieceColor.White));
            board.AddPiece(new KingPiece(new Position(4, 7), PieceColor.Black));

            var actualBoardDebugToString = board.ToString();
            actualBoardDebugToString.Should().Be(expectedBoard);
        }

        [Fact]
        public void SingleMoveTest()
        {
            var expectedBoard = @"RNBQKBNR
PPPPPPPP
--------
--------
-p------
--------
p-pppppp
rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(SimpleBoardParser.DefaultLayout);

            var from = new Position(1, 1);
            var to = new Position(1, 3);

            var pawnPiece = board.GetPieceAt(from);
            pawnPiece.Should().BeOfType<PawnPiece>();
            
            board.MovePieceTo(pawnPiece!, to);
            board.LastMovedPiece.Should().Be(pawnPiece);
            pawnPiece!.Position.Should().Be(to);

            var actualBoardDebugToString = board.ToString();
            actualBoardDebugToString.Should().Be(expectedBoard);
        }

        [Fact]
        public void SingleRemoveTest()
        {
            var expectedBoard = @"RNBQKBNR
PPPPPPPP
--------
--------
--------
--------
p-pppppp
rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(SimpleBoardParser.DefaultLayout);

            var cell = new Position(1, 1);

            var pawnPiece = board.GetPieceAt(cell);
            pawnPiece.Should().BeOfType<PawnPiece>();

            board.RemovePiece(pawnPiece!);
            
            var actualBoardDebugToString = board.ToString();
            actualBoardDebugToString.Should().Be(expectedBoard);
        }
    }
}

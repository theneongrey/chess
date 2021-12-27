using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class RookTest : APieceTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ---r----
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(0, 4),
                new Position(1, 4),
                new Position(2, 4),
                new Position(4, 4),
                new Position(5, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(3, 0),
                new Position(3, 1),
                new Position(3, 2),
                new Position(3, 3),
                new Position(3, 5),
                new Position(3, 6),
                new Position(3, 7)
            };

            AllowedMoves<RookPiece>(boardLayout, expectedMoves, new Position(3, 4));
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----r---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(0, 4),
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4),
                new Position(5, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(4, 2),
                new Position(4, 3),
                new Position(4, 5),
                new Position(4, 6)
            };

            AllowedMoves<RookPiece>(boardLayout, expectedMoves, new Position(4, 4));
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ---r----
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var rook = board.GetPieceAt(new Position(3, 4));

            var performedMoves = new[]
            {
                new Position(0, 4),
                new Position(1, 4),
                new Position(2, 4),
                new Position(4, 4),
                new Position(5, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(3, 0),
                new Position(3, 1),
                new Position(3, 2),
                new Position(3, 3),
                new Position(3, 5),
                new Position(3, 6),
                new Position(3, 7)
            };

            Assert.IsType<RookPiece>(rook);
            foreach (var move in performedMoves)
            {
                rook!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----r---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(0, 4),
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4),
                new Position(5, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(4, 2),
                new Position(4, 3),
                new Position(4, 5),
                new Position(4, 6)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var rook = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<RookPiece>(rook);
            foreach (var move in performedMoves)
            {
                rook!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         -p--r-p-
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var rook = board.GetPieceAt(new Position(4, 4));

            var performedMoves = new[]
            {
                new Position(4, 0),
                new Position(4, 1),
                new Position(4, 7),
                new Position(0, 4),
                new Position(1, 4),
                new Position(6, 4),
                new Position(7, 4)
            };

            Assert.IsType<RookPiece>(rook);
            foreach (var move in performedMoves)
            {
                rook!.IsMoveAllowed(board, move).Should().BeFalse();
            }
        }
    }
}

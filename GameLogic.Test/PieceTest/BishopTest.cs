using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class BishopTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----b---
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(3,3),
                new Position(2,2),
                new Position(1,1),
                new Position(0,0),
                new Position(5,3),
                new Position(6,2),
                new Position(7,1),
                new Position(5,5),
                new Position(6,6),
                new Position(7,7),
                new Position(3,5),
                new Position(2,6),
                new Position(1,7)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            var actualMoves = bishop!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----b---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(5,3),
                new Position(6,2),
                new Position(7,1),
                new Position(5,5),
                new Position(3,5),
                new Position(2,6),
                new Position(1,7)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            var actualMoves = bishop!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----b---
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(3, 3),
                new Position(2, 2),
                new Position(1, 1),
                new Position(0, 0),
                new Position(5, 3),
                new Position(6, 2),
                new Position(7, 1),
                new Position(5, 5),
                new Position(6, 6),
                new Position(7, 7),
                new Position(3, 5),
                new Position(2, 6),
                new Position(1, 7)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----b---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(5, 3),
                new Position(6, 2),
                new Position(7, 1),
                new Position(5, 5),
                new Position(3, 5),
                new Position(2, 6),
                new Position(1, 7)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----b---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";
            var performedMoves = new[]
            {
                new Position(3, 3),
                new Position(2, 2),
                new Position(1, 1),
                new Position(0, 0),
                new Position(6, 6),
                new Position(7, 7)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(board, move).Should().BeFalse();
            }
        }
    }
}

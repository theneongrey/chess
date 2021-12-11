using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KnightTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----n---
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(3,6),
                new Position(5,6),
                new Position(3,2),
                new Position(5,2),
                new Position(2,3),
                new Position(6,3),
                new Position(2,5),
                new Position(6,5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var knight = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            var actualMoves = knight!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----pP-
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(3,6),
                new Position(5,6),
                new Position(5,2),
                new Position(2,3),
                new Position(6,3),
                new Position(2,5),
                new Position(6,5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(bishop);
            var actualMoves = bishop!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----n---
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(3,6),
                new Position(5,6),
                new Position(3,2),
                new Position(5,2),
                new Position(2,3),
                new Position(6,3),
                new Position(2,5),
                new Position(6,5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var knight = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            foreach (var move in performedMoves)
            {
                knight!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(3, 6),
                new Position(5, 6),
                new Position(5, 2),
                new Position(2, 3),
                new Position(6, 3),
                new Position(2, 5),
                new Position(6, 5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var knight = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            foreach (var move in performedMoves)
            {
                knight!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";
            
            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var knight = board.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            knight!.IsMoveAllowed(board, new Position(3, 2)).Should().BeFalse();
        }
    }
}

using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KnightTest : PieceTest
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

            AllowedMoves<KnightPiece>(boardLayout, expectedMoves, new Position(4, 4));
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

            AllowedMoves<KnightPiece>(boardLayout, expectedMoves, new Position(4, 4));
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

using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class BishopTest : APieceTest
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

            AllowedMoves<BishopPiece>(boardLayout, expectedMoves, new Position(4, 4));
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

            AllowedMoves<BishopPiece>(boardLayout, expectedMoves, new Position(4, 4));
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

            MoveAreAllowed<BishopPiece>(boardLayout, new Position(4, 4), performedMoves);
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

            MoveAreAllowed<BishopPiece>(boardLayout, new Position(4, 4), performedMoves);
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

            MoveAreNotAllowed<BishopPiece>(boardLayout, new Position(4, 4), performedMoves);
        }
    }
}

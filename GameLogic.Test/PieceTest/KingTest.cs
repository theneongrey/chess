using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KingTest : APieceTest
    {
        [Fact]
        public void AllowedMovesForWhiteWithCastelingLeftAndRight()
        {
            const string boardLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var expectedMoves = new[]
            {
                new Position(3,0),
                new Position(5,0),
                new Position(3,1),
                new Position(5,1),
                new Position(2,0),
                new Position(6,0)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 0));
        }

        [Fact]
        public void AllowedMovesForBlackWithCastelingLeftAndRight()
        {
            const string boardLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var expectedMoves = new[]
            {
                new Position(3,7),
                new Position(5,7),
                new Position(3,6),
                new Position(5,6),
                new Position(2,7),
                new Position(6,7)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 7));
        }

        [Fact]
        public void AllowedMovesForWhiteWithCastelingBlockedLeftAndRight()
        {
            const string boardLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(3,0),
                new Position(3,1),
                new Position(5,1)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 0));
        }

        [Fact]
        public void AllowedMovesForBlackWithCastelingBlockedLeftAndRight()
        {
            const string boardLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(3,7),
                new Position(3,6),
                new Position(5,6)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 7));
        }

        [Fact]
        public void AllowedMovesForWhiteWithoutCasteling()
        {
            const string boardLayout = @"P---K---
                                         --------
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         ----k--p";

            var expectedMoves = new[]
            {
                new Position(3,0),
                new Position(5,0),
                new Position(3,1),
                new Position(5,1)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 0));
        }

        [Fact]
        public void AllowedMovesForBlackWithoutCasteling()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         ----k--p";

            var expectedMoves = new[]
            {
                new Position(3,7),
                new Position(5,7),
                new Position(3,6),
                new Position(5,6)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 7));
        }

        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         --------
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(3,2),
                new Position(3,3),
                new Position(3,4),
                new Position(4,2),
                new Position(4,4),
                new Position(5,2),
                new Position(5,3),
                new Position(5,4)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 3));
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----pP--
                                         ----k---
                                         ---p----
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(3,3),
                new Position(3,4),
                new Position(4,2),
                new Position(5,2),
                new Position(5,3),
                new Position(5,4)
            };

            AllowedMoves<KingPiece>(boardLayout, expectedMoves, new Position(4, 3));
        }

        [Fact]
        public void ForWhiteWithCastelingLeftAndRight_MoveIsAllowed()
        {
            const string boardLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var performedMoves = new[]
            {
                new Position(3, 0),
                new Position(5, 0),
                new Position(3, 1),
                new Position(5, 1),
                new Position(2, 0),
                new Position(6, 0)
            };

            MoveAreAllowed<KingPiece>(boardLayout, new Position(4, 0), performedMoves);
        }

        [Fact]
        public void ForBlackWithCastelingLeftAndRight_MoveIsAllowed()
        {
            const string boardLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var performedMoves = new[]
            {
                new Position(3, 7),
                new Position(5, 7),
                new Position(3, 6),
                new Position(5, 6),
                new Position(2, 7),
                new Position(6, 7)
            };

            MoveAreAllowed<KingPiece>(boardLayout, new Position(4, 7), performedMoves);
        }

        [Fact]
        public void ForWhiteWithObstaclesCastelingLeftAndRight_MoveIsNotAllowed()
        {
            const string boardLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var performedMoves = new[]
            {
                new Position(2, 0),
                new Position(6, 0)
            };

            MoveAreNotAllowed<KingPiece>(boardLayout, new Position(4, 0), performedMoves);
        }

        [Fact]
        public void ForBlackWithObstaclesCastelingLeftAndRight_MoveIsNotAllowed()
        {
            const string boardLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var performedMoves = new[]
            {
                new Position(2, 7),
                new Position(6, 7)
            };

            MoveAreNotAllowed<KingPiece>(boardLayout, new Position(4, 7), performedMoves);
        }

        [Fact]
        public void ForWhiteWithCastelingLeftAndRight_MoveIsNotAllowed()
        {
            const string boardLayout = @"P---K---
                                         --------
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         ----k--p";

            var performedMoves = new[]
            {
                new Position(2, 0),
                new Position(6, 0)
            };

            MoveAreNotAllowed<KingPiece>(boardLayout, new Position(4, 0), performedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         --------
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(3, 2),
                new Position(3, 3),
                new Position(3, 4),
                new Position(4, 2),
                new Position(4, 4),
                new Position(5, 2),
                new Position(5, 3),
                new Position(5, 4)
            };

            MoveAreAllowed<KingPiece>(boardLayout, new Position(4, 3), performedMoves);
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----pP--
                                         ----k---
                                         ---p----
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(3, 3),
                new Position(3, 4),
                new Position(4, 2),
                new Position(5, 2),
                new Position(5, 3),
                new Position(5, 4)
            };

            MoveAreAllowed<KingPiece>(boardLayout, new Position(4, 3), performedMoves);
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----pP--
                                         ----k---
                                         ---p----
                                         ----p---
                                         -------p";

            var performedMoves = new[]
            {
                new Position(4, 4)
            };

            MoveAreNotAllowed<KingPiece>(boardLayout, new Position(4, 3), performedMoves);
        }
    }
}

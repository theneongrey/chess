using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KingTest
    {
        [Fact]
        public void AllowedMovesForWhiteWithCastelingLeftAndRight()
        {
            const string fieldLayout = @"R---K--R
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
                new Position(0,0),
                new Position(7,0)
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForBlackWithCastelingLeftAndRight()
        {
            const string fieldLayout = @"R---K--R
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
                new Position(0,7),
                new Position(7,7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForWhiteWithoutCasteling()
        {
            const string fieldLayout = @"P---K---
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


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForBlackWithoutCasteling()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }
    }
}

using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class BishopTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            var actualMoves = bishop?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            var actualMoves = bishop?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         --------
                                         ----b---
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            bishop?.IsMoveAllowed(field, new Position(3, 3)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(2, 2)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(1, 1)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(0, 0)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(5, 3)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(6, 2)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(7, 1)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(5, 5)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(6, 6)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(7, 7)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(3, 5)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(2, 6)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(1, 7)).Should().BeTrue();
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----b---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            bishop?.IsMoveAllowed(field, new Position(5, 3)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(6, 2)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(7, 1)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(5, 5)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(3, 5)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(2, 6)).Should().BeTrue();
            bishop?.IsMoveAllowed(field, new Position(1, 7)).Should().BeTrue();
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----b---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            bishop?.IsMoveAllowed(field, new Position(3, 3)).Should().BeFalse();
            bishop?.IsMoveAllowed(field, new Position(2, 2)).Should().BeFalse();
            bishop?.IsMoveAllowed(field, new Position(1, 1)).Should().BeFalse();
            bishop?.IsMoveAllowed(field, new Position(0, 0)).Should().BeFalse();
            bishop?.IsMoveAllowed(field, new Position(6, 6)).Should().BeFalse();
            bishop?.IsMoveAllowed(field, new Position(7, 7)).Should().BeFalse();
        }
    }
}

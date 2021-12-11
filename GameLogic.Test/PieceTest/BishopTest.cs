using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.InternPieces;
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
            var actualMoves = bishop!.GetAllowedMoves(field);
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
            var actualMoves = bishop!.GetAllowedMoves(field);
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(field, move).Should().BeTrue();
            }
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(field, move).Should().BeTrue();
            }
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
            var performedMoves = new[]
            {
                new Position(3, 3),
                new Position(2, 2),
                new Position(1, 1),
                new Position(0, 0),
                new Position(6, 6),
                new Position(7, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<BishopPiece>(bishop);
            foreach (var move in performedMoves)
            {
                bishop!.IsMoveAllowed(field, move).Should().BeFalse();
            }
        }
    }
}

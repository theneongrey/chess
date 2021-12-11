using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KnightTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var knight = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            var actualMoves = knight!.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var bishop = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(bishop);
            var actualMoves = bishop!.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var knight = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            foreach (var move in performedMoves)
            {
                knight!.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var knight = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            foreach (var move in performedMoves)
            {
                knight!.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";
            
            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var knight = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<KnightPiece>(knight);
            knight!.IsMoveAllowed(field, new Position(3, 2)).Should().BeFalse();
        }
    }
}

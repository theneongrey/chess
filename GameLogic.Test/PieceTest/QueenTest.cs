using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class QueenTest
    {
        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string fieldLayout = @"----K---
                                         ----P---
                                         --------
                                         ---q----
                                         -----k--
                                         --------
                                         ----p---
                                         --------";

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
                new Position(3, 7),
                new Position(2, 3),
                new Position(1, 2),
                new Position(0, 1),
                new Position(4, 3),
                new Position(5, 2),
                new Position(6, 1),
                new Position(7, 0),
                new Position(4, 5),
                new Position(5, 6),
                new Position(6, 7),
                new Position(2, 5),
                new Position(1, 6),
                new Position(0, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var queen = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<QueenPiece>(queen);
            var actualMoves = queen?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string fieldLayout = @"R---K---
                                         ---P-P--
                                         --------
                                         -B-q--b-
                                         ----k---
                                         -p------
                                         ----p---
                                         ---r----";

            var expectedMoves = new[]
            {
                new Position(1, 4),
                new Position(2, 4),
                new Position(4, 4),
                new Position(5, 4),
                new Position(3, 1),
                new Position(3, 2),
                new Position(3, 3),
                new Position(3, 5),
                new Position(3, 6),
                new Position(2, 3),
                new Position(4, 5),
                new Position(5, 6),
                new Position(2, 5),
                new Position(1, 6),
                new Position(0, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var queen = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<QueenPiece>(queen);
            var actualMoves = queen?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"----K---
                                         ----P---
                                         --------
                                         ---q----
                                         -----k--
                                         --------
                                         ----p---
                                         --------";

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
                new Position(3, 7),
                new Position(2, 3),
                new Position(1, 2),
                new Position(0, 1),
                new Position(4, 3),
                new Position(5, 2),
                new Position(6, 1),
                new Position(7, 0),
                new Position(4, 5),
                new Position(5, 6),
                new Position(6, 7),
                new Position(2, 5),
                new Position(1, 6),
                new Position(0, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var queen = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<QueenPiece>(queen);
            foreach (var move in performedMoves)
            {
                queen?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"R---K---
                                         ---P-P--
                                         --------
                                         -B-q--b-
                                         ----k---
                                         -p------
                                         ----p---
                                         ---r----";

            var performedMoves = new[]
            {
                new Position(1, 4),
                new Position(2, 4),
                new Position(4, 4),
                new Position(5, 4),
                new Position(3, 1),
                new Position(3, 2),
                new Position(3, 3),
                new Position(3, 5),
                new Position(3, 6),
                new Position(2, 3),
                new Position(4, 5),
                new Position(5, 6),
                new Position(2, 5),
                new Position(1, 6),
                new Position(0, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var queen = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<QueenPiece>(queen);
            foreach (var move in performedMoves)
            {
                queen?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string fieldLayout = @"R---K---
                                         ---P-P--
                                         --------
                                         -B-q--b-
                                         ----k---
                                         -p------
                                         ----p---
                                         ---r----";

            var performedMoves = new[]
            {
                new Position(0, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(3, 0),
                new Position(3, 7),
                new Position(1, 2),
                new Position(0, 1),
                new Position(6, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var queen = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<QueenPiece>(queen);
            foreach (var move in performedMoves)
            {
                queen?.IsMoveAllowed(field, move).Should().BeFalse();
            }
        }
    }
}

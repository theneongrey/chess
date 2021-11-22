﻿using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class RookTest
    {
        [Fact]
        public void AllowedMovesForWhiteLeftRookWithCasteling()
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
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3),
                new Position(0, 4),
                new Position(0, 5),
                new Position(0, 6),
                new Position(0, 7),
                new Position(1, 0),
                new Position(2, 0),
                new Position(3, 0),
                new Position(4, 0)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 0));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForWhiteRightRookWithCasteling()
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
                new Position(7, 1),
                new Position(7, 2),
                new Position(7, 3),
                new Position(7, 4),
                new Position(7, 5),
                new Position(7, 6),
                new Position(7, 7),
                new Position(6, 0),
                new Position(5, 0),
                new Position(4, 0)
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 0));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForBlackLeftRookWithCasteling()
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
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3),
                new Position(0, 4),
                new Position(0, 5),
                new Position(0, 6),
                new Position(1, 7),
                new Position(2, 7),
                new Position(3, 7),
                new Position(4, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 7));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForBlackRightRookWithCasteling()
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
                new Position(7, 0),
                new Position(7, 1),
                new Position(7, 2),
                new Position(7, 3),
                new Position(7, 4),
                new Position(7, 5),
                new Position(7, 6),
                new Position(6, 7),
                new Position(5, 7),
                new Position(4, 7)
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 7));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForWhiteLeftRookWithCastelingBlocked()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3),
                new Position(0, 4),
                new Position(0, 5),
                new Position(0, 6),
                new Position(0, 7),
                new Position(1, 0)
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 0));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForWhiteRightRookWithCastelingBlocked()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(7, 1),
                new Position(7, 2),
                new Position(7, 3),
                new Position(7, 4),
                new Position(7, 5),
                new Position(7, 6),
                new Position(7, 7),
                new Position(6, 0)
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 0));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForLeftBlackRookWithCastelingBlocked()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(1,7),
                new Position(0,6),
                new Position(0,5),
                new Position(0,4),
                new Position(0,3),
                new Position(0,2),
                new Position(0,1),
                new Position(0,0),
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 7));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForRightBlackRookWithCastelingBlocked()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var expectedMoves = new[]
            {
                new Position(6,7),
                new Position(7,6),
                new Position(7,5),
                new Position(7,4),
                new Position(7,3),
                new Position(7,2),
                new Position(7,1),
                new Position(7,0),
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 7));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForWhiteWithoutCasteling()
        {
            const string fieldLayout = @"R--K----
                                         --------
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         ---k---r";

            var expectedMoves = new[]
            {
                new Position(7,7),
                new Position(7,6),
                new Position(7,5),
                new Position(7,4),
                new Position(7,3),
                new Position(7,2),
                new Position(7,1),
                new Position(6,0),
                new Position(5,0),
                new Position(4,0),
            };


            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 0));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesForBlackWithoutCasteling()
        {
            const string fieldLayout = @"R--K----
                                         --------
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         ---k---r";

            var expectedMoves = new[]
            {
                new Position(0, 6),
                new Position(0, 5),
                new Position(0, 4),
                new Position(0, 3),
                new Position(0, 2),
                new Position(0, 1),
                new Position(0, 0),
                new Position(1, 7),
                new Position(2, 7),
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 7));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithNoObstacles()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         --------
                                         ---r----
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

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
                new Position(3, 7)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedMovesWithObstacles()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----r---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var expectedMoves = new[]
            {
                new Position(0, 4),
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4),
                new Position(5, 4),
                new Position(6, 4),
                new Position(7, 4),
                new Position(4, 2),
                new Position(4, 3),
                new Position(4, 5),
                new Position(4, 6)
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<RookPiece>(rook);
            var actualMoves = rook?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void WithNoObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         --------
                                         ---r----
                                         ----k---
                                         --------
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(3, 4));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(0, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(1, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(2, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(4, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(5, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(6, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(7, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 0)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 1)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 2)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 3)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 5)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 6)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 7)).Should().BeTrue();
        }

        [Fact]
        public void WithObstacles_MoveIsAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         ----r---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(0, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(1, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(2, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(3, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(5, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(6, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(7, 4)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(4, 2)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(4, 3)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(4, 5)).Should().BeTrue();
            rook?.IsMoveAllowed(field, new Position(4, 6)).Should().BeTrue();
        }

        [Fact]
        public void WithObstacles_MoveIsNotAllowed()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         -p--r-p-
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(4, 4));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 0)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(4, 1)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(4, 7)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(0, 4)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(1, 4)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(6, 4)).Should().BeFalse();
            rook?.IsMoveAllowed(field, new Position(7, 4)).Should().BeFalse();
        }

        [Fact]
        public void ForWhiteLeftRookWithCasteling_MoveIsAllowed()
        {
            const string fieldLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 0));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 0)).Should().BeTrue();
        }

        [Fact]
        public void ForWhiteRightRookWithCasteling_MoveIsAllowed()
        {
            const string fieldLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 0));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 0)).Should().BeTrue();
        }

        [Fact]
        public void ForBlackLeftRookWithCasteling_MoveIsAllowed()
        {
            const string fieldLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 7));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 7)).Should().BeTrue();
        }

        [Fact]
        public void ForBlackRightRookWithCasteling_MoveIsAllowed()
        {
            const string fieldLayout = @"R---K--R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r---k--r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 7));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 7)).Should().BeTrue();
        }

        [Fact]
        public void ForWhiteLeftRookWithObstacleForCasteling_MoveIsNotAllowed()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 0));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 0)).Should().BeFalse();
        }

        [Fact]
        public void ForWhiteRightRookWithObstacleForCasteling_MoveIsANotAllowed()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 0));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 0)).Should().BeFalse();
        }

        [Fact]
        public void ForBlackLeftRookWithObstacleForCasteling_MoveIsNotAllowed()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(0, 7));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 7)).Should().BeFalse();
        }

        [Fact]
        public void ForBlackRightRookWithObstacleForCasteling_MoveIsNotAllowed()
        {
            const string fieldLayout = @"R-B-KB-R
                                         ----P---
                                         --------
                                         --------
                                         --------
                                         --------
                                         ----p---
                                         r-b-kb-r";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(7, 7));

            Assert.IsType<RookPiece>(rook);
            rook?.IsMoveAllowed(field, new Position(4, 7)).Should().BeFalse();
        }
    }
}

using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class KingTest
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


            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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


            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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


            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            var actualMoves = king!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            foreach (var move in performedMoves)
            {
                king!.IsMoveAllowed(board, move).Should().BeTrue();
            }
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            foreach (var move in performedMoves)
            {
                king!.IsMoveAllowed(board, move).Should().BeTrue();
            }
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            king!.IsMoveAllowed(board, new Position(2, 0)).Should().BeFalse();
            king!.IsMoveAllowed(board, new Position(6, 0)).Should().BeFalse();
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 7));

            Assert.IsType<KingPiece>(king);
            king!.IsMoveAllowed(board, new Position(2, 7)).Should().BeFalse();
            king!.IsMoveAllowed(board, new Position(6, 7)).Should().BeFalse();
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 0));

            Assert.IsType<KingPiece>(king);
            king!.IsMoveAllowed(board, new Position(2, 0)).Should().BeFalse();
            king!.IsMoveAllowed(board, new Position(6, 0)).Should().BeFalse();
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            foreach (var move in performedMoves)
            {
                king!.IsMoveAllowed(board, move).Should().BeTrue();
            }
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            foreach (var move in performedMoves)
            {
                king!.IsMoveAllowed(board, move).Should().BeTrue();
            }
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(4, 3));

            Assert.IsType<KingPiece>(king);
            king!.IsMoveAllowed(board, new Position(4, 4)).Should().BeFalse();
        }
    }
}

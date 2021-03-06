using FluentAssertions;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using System;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class PawnTest : APieceTest
    {
        [Fact]
        public void AllowedStartMovesWithNoObstacle()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(4,2),
                new Position(4,3),
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(4, 1));
        }

        [Fact]
        public void AllowedRegularMoveWithNoObstacle()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(1,3)
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(1, 2));
        }

        [Fact]
        public void AllowedStartMovesWithObstacle()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B---
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(4,2),
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(4, 1));
        }

        [Fact]
        public void AllowedStartMovesWithDirectObstacle_ShouldBeEmpty()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         ----B---
                                         pppppppp
                                         rnbqkbnr";

            NoAllowedMoves<PawnPiece>(boardLayout, new Position(4, 1));
        }

        [Fact]
        public void AllowedRegularMoveWithObstacle_ShouldBeEmpty()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         -B------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            NoAllowedMoves<PawnPiece>(boardLayout, new Position(1, 2));
        }

        [Fact]
        public void MovedTwoCellsForWhitePawn_AdvancedTwoCellsOnLastMove_ShouldBeTrue()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(1, 1))!;

            Assert.IsType<PawnPiece>(pawn);
            
            board.MovePieceTo(pawn, new Position(1, 3));
            ((PawnPiece)pawn).AdvancedTwoCellsOnLastMove.Should().BeTrue();
        }

        [Fact]
        public void MovedOneCellsForWhitePawn_AdvancedTwoCellsOnLastMove_ShouldBeFalse()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(1, 1))!;

            Assert.IsType<PawnPiece>(pawn);

            board.MovePieceTo(pawn, new Position(1, 2));
            ((PawnPiece)pawn).AdvancedTwoCellsOnLastMove.Should().BeFalse();
        }

        [Fact]
        public void MovedTwoCellsForBlackPawn_AdvancedTwoCellsOnLastMove_ShouldBeTrue()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(1, 6))!;

            Assert.IsType<PawnPiece>(pawn);

            board.MovePieceTo(pawn, new Position(1, 4));
            ((PawnPiece)pawn).AdvancedTwoCellsOnLastMove.Should().BeTrue();
        }

        [Fact]
        public void MovedOneCellsForBlackPawn_AdvancedTwoCellsOnLastMove_ShouldBeFalse()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(1, 6))!;

            Assert.IsType<PawnPiece>(pawn);

            board.MovePieceTo(pawn, new Position(1, 5));
            ((PawnPiece)pawn).AdvancedTwoCellsOnLastMove.Should().BeFalse();
        }

        [Fact]
        public void AllowedCaptureMovesForWhite()
        {
            const string boardLayout = @"RN-QK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B-B-
                                         -----p--
                                         ppppp-pp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(4, 3),
                new Position(5, 3),
                new Position(6, 3)
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(5, 2));
        }

        [Fact]
        public void AllowedCaptureMovesForBlack()
        {
            const string boardLayout = @"RN-QK-NR
                                         PP-PPPPP
                                         --P-----
                                         -b-b----
                                         ----B-B-
                                         -----p--
                                         ppppp-pp
                                         rn-qk-nr";

            var expectedMoves = new[]
            {
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4)
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(2, 5));
        }

        [Fact]
        public void AllowedEnPassantMovesForWhite()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         -----p--
                                         --------
                                         --------
                                         ppppp-pp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(5, 5),
                new Position(4, 5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(5, 4));
            var blackPawn = board.GetPieceAt(new Position(4, 6));

            board.MovePieceTo(blackPawn!, new Position(4, 4));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedEnPassantMovesForBlack()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --P-----
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(2, 2),
                new Position(1, 2)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(2, 3));
            var whitePawn = board.GetPieceAt(new Position(1,1));

            board.MovePieceTo(whitePawn!, new Position(1, 3));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn!.GetAllowedMoves(board);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }


        [Fact]
        public void AllowedMovesForWhite_WhenEnPassantIsNotPossible()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPP-PPP
                                         --------
                                         ----Pp--
                                         --------
                                         --------
                                         ppppp-pp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(5, 5)
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(5, 4));
        }

        [Fact]
        public void AllowedEnPassantMovesForBlack_WhenEnPassantIsNotPossible()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         -pP-----
                                         --------
                                         p-pppppp
                                         rnbqkbnr";

            var expectedMoves = new[]
            {
                new Position(2, 2)
            };

            AllowedMoves<PawnPiece>(boardLayout, expectedMoves, new Position(2, 3));
        }

        [Fact]
        public void StartMovesWithNoObstacle_MoveIsAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 2),
                new Position(4, 3),
            };

            MoveAreAllowed<PawnPiece>(boardLayout, new Position(4, 1), performedMoves);
        }

        [Fact]
        public void RegularMoveWithNoObstacle_MoveIsAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(1, 3),
            };

            MoveAreAllowed<PawnPiece>(boardLayout, new Position(1, 2), performedMoves);
        }

        [Fact]
        public void StartMovesWithObstacle_MoveIsAllowed()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B---
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 2),
            };

            MoveAreAllowed<PawnPiece>(boardLayout, new Position(4, 1), performedMoves);
        }


        [Fact]
        public void StartMovesWithObstacle_MoveIsNotAllowed()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B---
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 3),
            };

            MoveAreNotAllowed<PawnPiece>(boardLayout, new Position(4, 1), performedMoves);
        }

        [Fact]
        public void StartMovesWithDirectObstacle_MoveIsNotAllowed()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         ----B---
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 2),
                new Position(4, 3),
            };

            MoveAreNotAllowed<PawnPiece>(boardLayout, new Position(4, 1), performedMoves);
        }

        [Fact]
        public void RegularMoveWithObstacle_MoveIsNotAllowed()
        {
            const string boardLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         -B------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(1, 3),
            };

            MoveAreNotAllowed<PawnPiece>(boardLayout, new Position(1, 2), performedMoves);
        }

        [Fact]
        public void CaptureMovesForWhite_MoveIsAllowed()
        {
            const string boardLayout = @"RN-QK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B-B-
                                         -----p--
                                         ppppp-pp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 3),
                new Position(5, 3),
                new Position(6, 3)
            };

            MoveAreAllowed<PawnPiece>(boardLayout, new Position(5, 2), performedMoves);
        }

        [Fact]
        public void CaptureMovesForBlack_MoveIsAllowed()
        {
            const string boardLayout = @"RN-QK-NR
                                         PP-PPPPP
                                         --P-----
                                         -b-b----
                                         ----B-B-
                                         -----p--
                                         ppppp-pp
                                         rn-qk-nr";

            var performedMoves = new[]
            {
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4)
            };

            MoveAreAllowed<PawnPiece>(boardLayout, new Position(2, 5), performedMoves);
        }

        [Fact]
        public void EnPassantMovesForWhite_MoveIsAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         -----p--
                                         --------
                                         --------
                                         ppppp-pp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(5, 5),
                new Position(4, 5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(5, 4));
            var blackPawn = board.GetPieceAt(new Position(4, 6));

            board.MovePieceTo(blackPawn!, new Position(4, 4));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void EnPassantMovesForWhite_MoveIsNotAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PPPP-PPP
                                         --------
                                         ----Pp--
                                         --------
                                         --------
                                         ppppp-pp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4, 5)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(5, 4));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn!.IsMoveAllowed(board, move).Should().BeFalse();
            }
        }

        [Fact]
        public void EnPassantMovesForBlack_MoveIsAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PP-PPPPP
                                         --------
                                         --------
                                         --P-----
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(2, 2),
                new Position(1, 2)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(2, 3));
            var whitePawn = board.GetPieceAt(new Position(1, 1));

            board.MovePieceTo(whitePawn!, new Position(1, 3));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn!.IsMoveAllowed(board, move).Should().BeTrue();
            }
        }

        [Fact]
        public void EnPassantMovesForBlack_MoveIsNotAllowed()
        {
            const string boardLayout = @"RNBQKBNR
                                         PP-PPPPP
                                         --------
                                         --------
                                         --Pp----
                                         --------
                                         ppp-pppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(1, 2)
            };

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var pawn = board.GetPieceAt(new Position(2, 3));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn!.IsMoveAllowed(board, move).Should().BeFalse();
            }
        }
    }
}

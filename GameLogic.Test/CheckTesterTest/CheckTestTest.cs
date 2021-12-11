using FluentAssertions;
using GameLogic.CheckTester;
using GameLogic.BoardParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.CheckTesterTest
{
    public class CheckTestTest
    {
        [Fact]
        public void KingIsNotInDanger_ReturnsFalse()
        {
            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(SimpleBoardParser.DefaultLayout);
        
            CheckTest.IsKingInDanger(board, PieceColor.White).Should().BeFalse();
            CheckTest.IsKingInDanger(board, PieceColor.Black).Should().BeFalse();
        }

        [Fact]
        public void WhiteKingIsInDangerByPawn_ReturnsTrue()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         --P-n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);

            CheckTest.IsKingInDanger(board, PieceColor.White).Should().BeTrue();
        }

        [Fact]
        public void WhiteKingIsInDangerByQueen_ReturnsTrue()
        {
            const string boardLayout = @"P--QK---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);

            CheckTest.IsKingInDanger(board, PieceColor.White).Should().BeTrue();
        }

        [Fact]
        public void BlackKingIsInDangerByKnight_ReturnsTrue()
        {
            const string boardLayout = @"P---K---
                                         ----P---
                                         ---n-P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);

            CheckTest.IsKingInDanger(board, PieceColor.Black).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerTest_WhenPawnMovesTwoCells_DoesNotchangeBoardOrPieces()
        {
            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(SimpleBoardParser.DefaultLayout);
            var pawn = (PawnPiece)board.GetPieceAt(new Position(1, 1))!;

            CheckTest.WillKingBeInDanger(board, pawn!, new Position(1, 3));

            var actualBoardDebugToString = board.ToString();
            actualBoardDebugToString.Should().Be(SimpleBoardParser.DefaultLayout);
            board.LastMovedPiece.Should().BeNull();
            board.GetPieceAt(new Position(1, 3)).Should().BeNull();

            pawn.WasMoved.Should().BeFalse();
            pawn.AdvancedTwoCellsOnLastMove.Should().BeFalse();
            pawn.Position.Should().Be(new Position(1,1));
        }

        [Fact]
        public void KingWillBeInDangerTest_AfterCapturing_DoesNotchangeBoarddOrPieces()
        {
            const string boardLayout = @"RNBQKBNR
PPP-PPPP
--------
---P----
----p---
--------
pppp-ppp
rnbqkbnr";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var whitePawn = (PawnPiece)board.GetPieceAt(new Position(4, 3))!;
            var blackPawn = (PawnPiece)board.GetPieceAt(new Position(3, 4))!;

            CheckTest.WillKingBeInDanger(board, whitePawn!, new Position(3, 4));

            var actualBoardDebugToString = board.ToString();
            actualBoardDebugToString.Should().Be(boardLayout);
            board.LastMovedPiece.Should().BeNull();
            board.GetPieceAt(new Position(4, 3)).Should().Be(whitePawn);
            board.GetPieceAt(new Position(3, 4)).Should().Be(blackPawn);

            whitePawn.WasMoved.Should().BeFalse();
            blackPawn.WasMoved.Should().BeFalse();
            whitePawn.Position.Should().Be(new Position(4, 3));
            blackPawn.Position.Should().Be(new Position(3, 4));
        }

        [Fact]
        public void KingWillNotBeInDanger_ReturnsFalse()
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
            var knight = board.GetPieceAt(new Position(1, 0));

            CheckTest.WillKingBeInDanger(board, knight!, new Position(2, 2)).Should().BeFalse();
        }

        [Fact]
        public void KingWillBeInDangerWhenKingMoves_ReturnsTrue()
        {
            const string boardLayout = @"P-Q-K---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(3, 3));

            CheckTest.WillKingBeInDanger(board, king!, new Position(2, 3)).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerWhenKingMoves_WithAvoidingRecursiveCheck_ReturnsTrue()
        {
            // when the king moves to 2,3 it's threatened by the queen
            // normally she could not move, because then her queen would be threatened by the white rook
            // but this thread should be insignificant and don't block the queens possible moves

            const string boardLayout = @"r-Q-K---
                                         --------
                                         --------
                                         --------
                                         ---k----
                                         --------
                                         --------
                                         --------";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var king = board.GetPieceAt(new Position(3, 3));

            CheckTest.WillKingBeInDanger(board, king!, new Position(2, 3)).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerWhenOtherPieceMoves_ReturnsTrue()
        {
            const string boardLayout = @"---QK---
                                         ---r----
                                         --------
                                         --------
                                         ---k----
                                         --------
                                         --------
                                         --------";

            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var rook = board.GetPieceAt(new Position(3, 6));

            CheckTest.WillKingBeInDanger(board, rook!, new Position(2, 6)).Should().BeTrue();
        }
    }
}

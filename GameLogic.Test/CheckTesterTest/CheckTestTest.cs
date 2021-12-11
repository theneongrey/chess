using FluentAssertions;
using GameLogic.CheckTester;
using GameLogic.FieldParser;
using GameLogic.InternPieces;
using Xunit;

namespace GameLogic.Test.CheckTesterTest
{
    public class CheckTestTest
    {
        [Fact]
        public void KingIsNotInDanger_ReturnsFalse()
        {
            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);
        
            CheckTest.IsKingInDanger(field, PieceColor.White).Should().BeFalse();
            CheckTest.IsKingInDanger(field, PieceColor.Black).Should().BeFalse();
        }

        [Fact]
        public void WhiteKingIsInDangerByPawn_ReturnsTrue()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         -----P--
                                         --P-n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);

            CheckTest.IsKingInDanger(field, PieceColor.White).Should().BeTrue();
        }

        [Fact]
        public void WhiteKingIsInDangerByQueen_ReturnsTrue()
        {
            const string fieldLayout = @"P--QK---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);

            CheckTest.IsKingInDanger(field, PieceColor.White).Should().BeTrue();
        }

        [Fact]
        public void BlackKingIsInDangerByKnight_ReturnsTrue()
        {
            const string fieldLayout = @"P---K---
                                         ----P---
                                         ---n-P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);

            CheckTest.IsKingInDanger(field, PieceColor.Black).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerTest_WhenPawnMovesTwoCells_DoesNotchangeFieldOrPieces()
        {
            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);
            var pawn = (PawnPiece)field.GetPieceAt(new Position(1, 1))!;

            CheckTest.WillKingBeInDanger(field, pawn!, new Position(1, 3));

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(SingleBoardSimpleStringLayoutParser.DefaultLayout);
            field.LastMovedPiece.Should().BeNull();
            field.GetPieceAt(new Position(1, 3)).Should().BeNull();

            pawn.WasMoved.Should().BeFalse();
            pawn.AdvancedTwoCellsOnLastMove.Should().BeFalse();
            pawn.Position.Should().Be(new Position(1,1));
        }

        [Fact]
        public void KingWillBeInDangerTest_AfterCapturing_DoesNotchangeFieldOrPieces()
        {
            const string fieldLayout = @"RNBQKBNR
PPP-PPPP
--------
---P----
----p---
--------
pppp-ppp
rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var whitePawn = (PawnPiece)field.GetPieceAt(new Position(4, 3))!;
            var blackPawn = (PawnPiece)field.GetPieceAt(new Position(3, 4))!;

            CheckTest.WillKingBeInDanger(field, whitePawn!, new Position(3, 4));

            var actualFieldDebugToString = field.ToString();
            actualFieldDebugToString.Should().Be(fieldLayout);
            field.LastMovedPiece.Should().BeNull();
            field.GetPieceAt(new Position(4, 3)).Should().Be(whitePawn);
            field.GetPieceAt(new Position(3, 4)).Should().Be(blackPawn);

            whitePawn.WasMoved.Should().BeFalse();
            blackPawn.WasMoved.Should().BeFalse();
            whitePawn.Position.Should().Be(new Position(4, 3));
            blackPawn.Position.Should().Be(new Position(3, 4));
        }

        [Fact]
        public void KingWillNotBeInDanger_ReturnsFalse()
        {
            const string fieldLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var knight = field.GetPieceAt(new Position(1, 0));

            CheckTest.WillKingBeInDanger(field, knight!, new Position(2, 2)).Should().BeFalse();
        }

        [Fact]
        public void KingWillBeInDangerWhenKingMoves_ReturnsTrue()
        {
            const string fieldLayout = @"P-Q-K---
                                         ----P---
                                         -----P--
                                         ----n---
                                         ---k----
                                         ---p----
                                         ----p---
                                         -------p";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(3, 3));

            CheckTest.WillKingBeInDanger(field, king!, new Position(2, 3)).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerWhenKingMoves_WithAvoidingRecursiveCheck_ReturnsTrue()
        {
            // when the king moves to 2,3 it's threatened by the queen
            // normally she could not move, because then her queen would be threatened by the white rook
            // but this thread should be insignificant and don't block the queens possible moves

            const string fieldLayout = @"r-Q-K---
                                         --------
                                         --------
                                         --------
                                         ---k----
                                         --------
                                         --------
                                         --------";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var king = field.GetPieceAt(new Position(3, 3));

            CheckTest.WillKingBeInDanger(field, king!, new Position(2, 3)).Should().BeTrue();
        }

        [Fact]
        public void KingWillBeInDangerWhenOtherPieceMoves_ReturnsTrue()
        {
            const string fieldLayout = @"---QK---
                                         ---r----
                                         --------
                                         --------
                                         ---k----
                                         --------
                                         --------
                                         --------";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var rook = field.GetPieceAt(new Position(3, 6));

            CheckTest.WillKingBeInDanger(field, rook!, new Position(2, 6)).Should().BeTrue();
        }
    }
}

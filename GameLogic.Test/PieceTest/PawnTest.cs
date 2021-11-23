using FluentAssertions;
using GameLogic.FieldParser;
using GameLogic.Pieces;
using Xunit;

namespace GameLogic.Test.PieceTest
{
    public class PawnTest
    {
        [Fact]
        public void AllowedStartMovesWithNoObstacle()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedRegularMoveWithNoObstacle()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(1, 2));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedStartMovesWithObstacle()
        {
            const string fieldLayout = @"RNBQK-NR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedStartMovesWithDirectObstacle_ShouldBeEmpty()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         ----B---
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().BeEmpty();
        }

        [Fact]
        public void AllowedRegularMoveWithObstacle_ShouldBeEmpty()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         -B------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(1, 2));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().BeEmpty();
        }

        [Fact]
        public void AllowedCaptureMovesForWhite()
        {
            const string fieldLayout = @"RN-QK-NR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 2));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedCaptureMovesForBlack()
        {
            const string fieldLayout = @"RN-QK-NR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 5));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedEnPassantMovesForWhite()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 4));

            field.MovePiece(new Position(4, 6), new Position(4, 4));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedEnPassantMovesForBlack()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 3));

            field.MovePiece(new Position(1, 1), new Position(1, 3));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }


        [Fact]
        public void AllowedMovesForWhite_WhenEnPassantIsNotPossible()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 4));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }

        [Fact]
        public void AllowedEnPassantMovesForBlack_WhenEnPassantIsNotPossible()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 3));

            Assert.IsType<PawnPiece>(pawn);
            var actualMoves = pawn?.GetAllowedMoves(field);
            actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
        }





        [Fact]
        public void StartMovesWithNoObstacle_MoveIsAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4,2),
                new Position(4,3),
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void RegularMoveWithNoObstacle_MoveIsAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(1, 2));

            Assert.IsType<PawnPiece>(pawn);
            pawn?.IsMoveAllowed(field, new Position(1, 3)).Should().BeTrue();
        }

        [Fact]
        public void StartMovesWithObstacle_MoveIsAllowed()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B---
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            pawn?.IsMoveAllowed(field, new Position(4, 2)).Should().BeTrue();
        }


        [Fact]
        public void StartMovesWithObstacle_MoveIsNotAllowed()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         ----B---
                                         --------
                                         pppppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            pawn?.IsMoveAllowed(field, new Position(4, 3)).Should().BeFalse();
        }

        [Fact]
        public void StartMovesWithDirectObstacle_MoveIsNotAllowed()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         --------
                                         ----B---
                                         pppppppp
                                         rnbqkbnr";

            var performedMoves = new[]
            {
                new Position(4,2),
                new Position(4,3),
            };

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(4, 1));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeFalse();
            }
        }

        [Fact]
        public void RegularMoveWithObstacle_MoveIsNotAllowed()
        {
            const string fieldLayout = @"RNBQK-NR
                                         PPPPPPPP
                                         --------
                                         --------
                                         -B------
                                         -p------
                                         p-pppppp
                                         rnbqkbnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(1, 2));

            Assert.IsType<PawnPiece>(pawn);
            pawn?.IsMoveAllowed(field, new Position(1, 3)).Should().BeFalse();
        }

        [Fact]
        public void CaptureMovesForWhite_MoveIsAllowed()
        {
            const string fieldLayout = @"RN-QK-NR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 2));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void CaptureMovesForBlack_MoveIsAllowed()
        {
            const string fieldLayout = @"RN-QK-NR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 5));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void EnPassantMovesForWhite_MoveIsAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 4));

            field.MovePiece(new Position(4, 6), new Position(4, 4));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void EnPassantMovesForWhite_MoveIsNotAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(5, 4));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeFalse();
            }
        }

        [Fact]
        public void EnPassantMovesForBlack_MoveIsAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
                                         PPPPPPPP
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 3));

            field.MovePiece(new Position(1, 1), new Position(1, 3));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeTrue();
            }
        }

        [Fact]
        public void EnPassantMovesForBlack_MoveIsNotAllowed()
        {
            const string fieldLayout = @"RNBQKBNR
                                         PPPPPPPP
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

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(fieldLayout);
            var pawn = field.GetPieceAt(new Position(2, 3));

            Assert.IsType<PawnPiece>(pawn);
            foreach (var move in performedMoves)
            {
                pawn?.IsMoveAllowed(field, move).Should().BeFalse();
            }
        }
    }
}

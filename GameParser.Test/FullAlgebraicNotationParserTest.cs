using FluentAssertions;
using GameLogic;
using Xunit;

namespace GameParser.Test
{
    public class FullAlgebraicNotationParserTest
    {
        [Fact]
        public void AfterParsing_BoardShouldBeSet()
        {
            var input = @"1.f2-f4     e7-e5
2.f4xe5     d7-d6
3.e5xd6     Bf8xd6
4.g2-g3     Qd8-g5
5.Ng1-f3    Qg5xg3+
6.h2xg3     Bd6xg3#";

            var expectedBoard = @"RNB-K-NR
PPP--PPP
--------
--------
--------
-----nB-
ppppp---
rnbqkb-r";

            var game = FullAlgebraicNotationParser.GetGameFromNotation(input);
            var actualBoard = game.ToString();

            actualBoard.Should().Be(expectedBoard);
        }

        [Fact]
        public void AfterCastelingOnQueenSide_RookShouldEndOnX3()
        {
            var input = @"1.d2-d4 d7-d6
2.Bc1-f4 e7-e6
3.Qd1-d3 c7-c6
4.Nb1-a3 b7-b6
5.O-O-O";

            var game = FullAlgebraicNotationParser.GetGameFromNotation(input);
            game.GetPieceAtCell(new Position(0, 0)).Should().BeNull();
            game.GetPieceAtCell(new Position(1, 0)).Should().BeNull();
            game.GetPieceAtCell(new Position(2, 0)).Should().Be(GamePieces.WhiteKing);
            game.GetPieceAtCell(new Position(3, 0)).Should().Be(GamePieces.WhiteRook);
            game.GetPieceAtCell(new Position(4, 0)).Should().BeNull();
        }

        [Fact]
        public void AfterCastelingOnKingSide_RookShouldEndOnX5()
        {
            var input = @"11.g2-g4 g7-g6
2.Ng1-h3 f7-f5
3.Bf1-g2 h7-h6
4.O-O";

            var game = FullAlgebraicNotationParser.GetGameFromNotation(input);
            game.GetPieceAtCell(new Position(7, 0)).Should().BeNull();
            game.GetPieceAtCell(new Position(6, 0)).Should().Be(GamePieces.WhiteKing);
            game.GetPieceAtCell(new Position(5, 0)).Should().Be(GamePieces.WhiteRook);
            game.GetPieceAtCell(new Position(4, 0)).Should().BeNull();
        }
    }
}
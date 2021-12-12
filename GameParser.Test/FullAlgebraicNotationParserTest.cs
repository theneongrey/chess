using FluentAssertions;
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
    }
}
using FluentAssertions;
using Xunit;

namespace GameParser.Test
{
    public class FullAlgebraicNotationOutputTest
    {
        [Fact]
        public void AfterParsing_OutputShouldBeEqual()
        {
            var input = @"1.f2-f4 e7-e5
2.f4xe5 d7-d6
3.e5xd6 Bf8xd6
4.g2-g3 Qd8-g5
5.Ng1-f3 Qg5xg3+
6.h2xg3 Bd6xg3#";

            var game = FullAlgebraicNotationParser.GetGameFromNotation(input);
            var notationOutput = game.ToFullAlgebraicNotation();

            notationOutput.Should().Be(input);
        }

        [Fact]
        public void AfterParsing_OutputShouldBeEqual2()
        {
            var input = @"1.e2-e4 f7-f5
2.e4xf5 d7-d5
3.f5-f6 d5-d4
4.f6-f7+ Ke8xf7
5.Bf1-c4+ Bc8-e6
6.Bc4-d5 Qd8-e8
7.Qd1-e2 a7-a6
8.Bd5xe6+ Kf7-f6
9.Qe2-f3+ Kf6-g5
10.Qf3-d5+ Kg5-h6
11.d2-d3+ Kh6-g6
12.Qd5-g5#";

            var game = FullAlgebraicNotationParser.GetGameFromNotation(input);
            var notationOutput = game.ToFullAlgebraicNotation();

            notationOutput.Should().Be(input);
        }
    }
}

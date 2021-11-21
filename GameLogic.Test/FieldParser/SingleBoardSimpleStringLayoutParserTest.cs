using FluentAssertions;
using GameLogic.FieldParser;
using System;
using Xunit;

namespace GameLogic.Test.FieldParser
{
    public class SingleBoardSimpleStringLayoutParserTest
    {
        [Fact]
        public void DefaultLayoutTest()
        {
            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);
            var actualFieldDebugToString = field.ToString();

            actualFieldDebugToString.Should().Be(SingleBoardSimpleStringLayoutParser.DefaultLayout);
        }

        [Fact]
        public void RandomLayoutTest()
        {
            var randomLayout = @"Q--RKBNR
P-PP-PPP
B-N-P---
-P------
-----p--
--n-p---
ppppk-pp
r-bq-bnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(randomLayout);
            var actualFieldDebugToString = field.ToString();

            actualFieldDebugToString.Should().Be(randomLayout);
        }

        [Fact]
        public void WrongRowCountInput_ShouldFail()
        {
            var randomLayout = @"--------
                                 --------
                                 --------
                                 -----p--
                                 --n-p---
                                 ppppk-pp
                                 r-bq-bnr";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("The input must contain exactly 8 rows.");
        }

        [Fact]
        public void WrongCellCountInput_ShouldFail()
        {
            var randomLayout = @"------
                                 ------
                                 ------
                                 ------
                                 ------
                                 ---p--
                                 ---p--
                                 ---k--";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Every row must contain 8 cells. (6 cells found at row 0)");
        }

        [Fact]
        public void UnknownCharacterInput_ShouldFail()
        {
            var randomLayout = @"x--k----
                                 ---K----
                                 --------
                                 --------
                                 --------
                                 --------
                                 --------
                                 --------";

            var simpleStringLayoutParser = new SingleBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Character x at position X=0, Y=7 is not valid");
        }
    }
}

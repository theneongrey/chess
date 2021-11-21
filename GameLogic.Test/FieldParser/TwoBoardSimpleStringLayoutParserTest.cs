using FluentAssertions;
using GameLogic.FieldParser;
using System;
using Xunit;

namespace GameLogic.Test.FieldParser
{
    public class TwoBoardSimpleStringLayoutParserTest
    {
        [Fact]
        public void DefaultLayoutTest()
        {
            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(TwoBoardSimpleStringLayoutParser.DefaultLayout);
            var actualFieldDebugToString = field.ToString();

            actualFieldDebugToString.Should().Be(SingleBoardSimpleStringLayoutParser.DefaultLayout);
        }

        [Fact]
        public void RandomLayoutTest()
        {
            var randomLayout = @"--------
                                 --------
                                 --------
                                 --------
                                 -----p--
                                 --n-p---
                                 ppppk-pp
                                 r-bq-bnr;
                                 q--rkbnr
                                 p-pp-ppp
                                 b-n-p---
                                 -p------
                                 --------
                                 --------
                                 --------
                                 --------
";

        var expectedFieldDebugToString = @"Q--RKBNR
P-PP-PPP
B-N-P---
-P------
-----p--
--n-p---
ppppk-pp
r-bq-bnr";

            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(randomLayout);
            var actualFieldDebugToString = field.ToString();

            actualFieldDebugToString.Should().Be(expectedFieldDebugToString);
        }

        [Fact]
        public void OnlyOneColorInput_ShouldFail()
        {
            var randomLayout = @"--------
                                 --------
                                 --------
                                 --------
                                 -----p--
                                 --n-p---
                                 ppppk-pp
                                 r-bq-bnr";

            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("The input must have two sides separated by ';'");
        }

        [Fact]
        public void WrongCellCountInput_ShouldFail()
        {
            var randomLayout = @"------;
                                 ------";

            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Every row must contain 8 cells. (6 cells found at row 0)");
        }


        [Fact]
        public void CellWasSetTwiceInput_ShouldFail()
        {
            var randomLayout = @"---k----;
                                 ---k----";

            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("The cell at position 3,7 is set twice.");
        }

        [Fact]
        public void UnknownCharacterInput_ShouldFail()
        {
            var randomLayout = @"x--k----;
                                 ---k----";

            var simpleStringLayoutParser = new TwoBoardSimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Character x at position X=0, Y=7 for color White is not valid");
        }
    }
}

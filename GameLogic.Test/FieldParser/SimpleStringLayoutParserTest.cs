using FluentAssertions;
using GameLogic.FieldParser;
using System;
using Xunit;

namespace GameLogic.Test.FieldParser
{
    public class SimpleStringLayoutParserTest
    {
        [Fact]
        public void DefaultLayoutTest()
        {
            var expectedFieldDebugToString = @"BRBNBBBQBKBBBNBR
BPBPBPBPBPBPBPBP
                
                
                
                
WPWPWPWPWPWPWPWP
WRWNWBWQWKWBWNWR
";

            var simpleStringLayoutParser = new SimpleStringLayoutParser();
            var field = simpleStringLayoutParser.CreateField(SimpleStringLayoutParser.DefaultLayout);
            var actualFieldDebugToString = field.ToString();

            actualFieldDebugToString.Should().Be(expectedFieldDebugToString);
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

        var expectedFieldDebugToString = @"BQ    BRBKBBBNBR
BP  BPBP  BPBPBP
BB  BN  BP      
  BP            
          WP    
    WN  WP      
WPWPWPWPWK  WPWP
WR  WBWQ  WBWNWR
";

            var simpleStringLayoutParser = new SimpleStringLayoutParser();
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

            var simpleStringLayoutParser = new SimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("The input must have two sides separated by ';'");
        }

        [Fact]
        public void WrongCellCountInput_ShouldFail()
        {
            var randomLayout = @"------;
                                 ------";

            var simpleStringLayoutParser = new SimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Every row must contain 8 cells. (6 cells found at row 0)");
        }


        [Fact]
        public void CellWasSetTwiceInput_ShouldFail()
        {
            var randomLayout = @"---k----;
                                 ---k----";

            var simpleStringLayoutParser = new SimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("The cell at position 3,7 is set twice.");
        }

        [Fact]
        public void UnknownCharacterInput_ShouldFail()
        {
            var randomLayout = @"x--k----;
                                 ---k----";

            var simpleStringLayoutParser = new SimpleStringLayoutParser();

            Action act = () => simpleStringLayoutParser.CreateField(randomLayout);

            act.Should().Throw<FieldParserException>()
                .WithMessage("Character x at position X=0, Y=7 for color White is not valid");
        }
    }
}

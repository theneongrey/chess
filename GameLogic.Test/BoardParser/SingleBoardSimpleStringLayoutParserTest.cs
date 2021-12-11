using FluentAssertions;
using GameLogic.BoardParser;
using System;
using Xunit;

namespace GameLogic.Test.BoardParser
{
    public class SingleBoardSimpleStringLayoutParserTest
    {
        [Fact]
        public void DefaultLayoutTest()
        {
            var simpleStringLayoutParser = new SimpleBoardParser();
            var boardParser = simpleStringLayoutParser.CreateBoard(SimpleBoardParser.DefaultLayout);
            var actualBoardDebugToString = boardParser.ToString();

            actualBoardDebugToString.Should().Be(SimpleBoardParser.DefaultLayout);
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

            var simpleStringLayoutParser = new SimpleBoardParser();
            var boardParser = simpleStringLayoutParser.CreateBoard(randomLayout);
            var actualBoardDebugToString = boardParser.ToString();

            actualBoardDebugToString.Should().Be(randomLayout);
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

            var simpleStringLayoutParser = new SimpleBoardParser();

            Action act = () => simpleStringLayoutParser.CreateBoard(randomLayout);

            act.Should().Throw<BoardParserException>()
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

            var simpleStringLayoutParser = new SimpleBoardParser();

            Action act = () => simpleStringLayoutParser.CreateBoard(randomLayout);

            act.Should().Throw<BoardParserException>()
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

            var simpleStringLayoutParser = new SimpleBoardParser();

            Action act = () => simpleStringLayoutParser.CreateBoard(randomLayout);

            act.Should().Throw<BoardParserException>()
                .WithMessage("Character x at position X=0, Y=7 is not valid");
        }
    }
}

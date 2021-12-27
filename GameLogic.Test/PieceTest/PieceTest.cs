using FluentAssertions;
using GameLogic.BoardParser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameLogic.Test.PieceTest
{

    public abstract class PieceTest
    {
        protected void AllowedMoves<PieceType>(string boardLayout, IEnumerable<Position> expectedMoves, Position initialPosition)
        {
            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(initialPosition);

            Assert.IsType<PieceType>(bishop);
            var actualMoves = bishop!.GetAllowedMoves(board);

            if (expectedMoves.Any())
            {
                actualMoves.Should().HaveSameCount(expectedMoves).And.Contain(expectedMoves);
            }
            else
            {
                actualMoves.Should().BeEmpty();
            }
        }
    }
}

using FluentAssertions;
using GameLogic.BoardParser;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameLogic.Test.PieceTest
{

    public abstract class APieceTest
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

        protected void NoAllowedMoves<PieceType>(string boardLayout, Position initialPosition)
        {
            AllowedMoves<PieceType>(boardLayout, Array.Empty<Position>(), initialPosition);
        }

        private void CheckMovesAllowed<PieceType>(string boardLayout, Position piecePosition, IEnumerable<Position> moves, bool isAllowed)
        {
            var simpleStringLayoutParser = new SimpleBoardParser();
            var board = simpleStringLayoutParser.CreateBoard(boardLayout);
            var bishop = board.GetPieceAt(piecePosition);

            Assert.IsType<PieceType>(bishop);
            foreach (var move in moves)
            {
                if (isAllowed)
                {
                    bishop!.IsMoveAllowed(board, move).Should().BeTrue();
                }
                else
                {
                    bishop!.IsMoveAllowed(board, move).Should().BeFalse();
                }
            }
        }

        protected void MoveAreAllowed<PieceType>(string boardLayout, Position piecePosition, IEnumerable<Position> moves)
        {
            CheckMovesAllowed<PieceType>(boardLayout, piecePosition, moves, true);
        }

        protected void MoveAreNotAllowed<PieceType>(string boardLayout, Position piecePosition, IEnumerable<Position> moves)
        {
            CheckMovesAllowed<PieceType>(boardLayout, piecePosition, moves, false);
        }
    }
}

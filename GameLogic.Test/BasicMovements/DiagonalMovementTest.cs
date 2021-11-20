using FluentAssertions;
using GameLogic.BasicMovements;
using System.Linq;
using Xunit;

namespace GameLogic.Test.BasicMovements
{
    public class DiagonalMovementTest
    {
        [Fact]
        public void KingAllowedDiagonalPositions()
        {
            var movement = new DiagonalMovement(1);
            var actualMovements = movement.GetAllowedPositions(new Position(5, 5)).ToArray();
            var expectedMovements = new[]
            {
                new[] { new Position(6,6) },
                new[] { new Position(4,6) },
                new[] { new Position(6,4) },
                new[] { new Position(4,4) },
            };

            actualMovements.Should().HaveSameCount(expectedMovements);
            for (var i = 0; i < expectedMovements.Length; i++)
            {
                actualMovements[i].Should().HaveSameCount(expectedMovements[i]).And.ContainInOrder(expectedMovements[i]);
            }
        }

        [Fact]
        public void KingIsMovementAllowed_IsTrue()
        {
            var movement = new DiagonalMovement(1);
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 6)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 6)).Should().BeTrue();
        }

        [Fact]
        public void KingIsMovementAllowed_IsFalse()
        {
            var movement = new DiagonalMovement(1);
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 7)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 3)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(1, 3)).Should().BeFalse();
        }

        [Fact]
        public void BishopAllowedPosition()
        {
            var movement = new DiagonalMovement();
            var actualMovements = movement.GetAllowedPositions(new Position(1, 2)).ToArray();

            var expectedMovements = new[]
            {
                new []{
                    new Position(2,3),
                    new Position(3,4),
                    new Position(4,5),
                    new Position(5,6),
                    new Position(6,7)
                },
                new []{
                    new Position(0,3),
                },
                new []{
                    new Position(2,1),
                    new Position(3,0)
                },
                new[] {
                    new Position(0,1),
                }
            };

            actualMovements.Should().HaveSameCount(expectedMovements);
            for (var i = 0; i < expectedMovements.Length; i++)
            {
                actualMovements[i].Should().HaveSameCount(expectedMovements[i]).And.ContainInOrder(expectedMovements[i]);
            }
        }

        [Fact]
        public void BishopIsMovementAllowed_IsTrue()
        {
            var movement = new DiagonalMovement();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 6)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 7)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(0, 0)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 6)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 7)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 3)).Should().BeTrue();
        }

        [Fact]
        public void BishopIsMovementAllowed_IsFalse()
        {
            var movement = new DiagonalMovement();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(0, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(1, 7)).Should().BeFalse();
        }
    }
}

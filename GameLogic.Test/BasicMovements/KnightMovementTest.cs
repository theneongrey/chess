using FluentAssertions;
using GameLogic.BasicMovements;
using System.Linq;
using Xunit;

namespace GameLogic.Test.BasicMovements
{
    public class KnightMovementTest
    {
        [Fact]
        public void KnightAllowedPositions()
        {
            var movement = new JumpMovement();
            var actualMovements = movement.GetAllowedPositions(new Position(5, 5)).ToArray();
            var expectedMovements = new[]
            {
                new [] { new Position(6,7) },
                new [] { new Position(4,7) },
                new [] { new Position(4,3) },
                new [] { new Position(6,3) },
                new [] { new Position(7,6) },
                new [] { new Position(3,6) },
                new [] { new Position(3,4) },
                new [] { new Position(7,4) }
            };

            actualMovements.Should().HaveSameCount(expectedMovements);
            for (var i = 0; i < expectedMovements.Length; i++)
            {
                actualMovements[i].Should().HaveSameCount(expectedMovements[i]).And.Contain(expectedMovements[i]);
            }
        }

        [Fact]
        public void KingIsMovementAllowed_IsTrue()
        {
            var movement = new JumpMovement();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 7)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 7)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 3)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 3)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 6)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 6)).Should().BeTrue();
        }

        [Fact]
        public void KingIsMovementAllowed_IsFalse()
        {
            var movement = new JumpMovement();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(4, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 7)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(3, 3)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(1, 3)).Should().BeFalse();
        }

    }
}

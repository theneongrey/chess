using FluentAssertions;
using GameLogic.BasicMovements;
using System.Linq;
using Xunit;

namespace GameLogic.Test.BasicMovements
{
    public class HorizontalVerticalMovementTest
    {
        [Fact]
        public void KingAllowedHVPositions()
        {
            var movement = new HorizontalVerticalMovement(1);
            var actualMovements = movement.GetAllowedPositions(new Position(5, 5)).ToArray();
            var expectedMovements = new[]
            {
                new [] { new Position(5,6) },
                new [] { new Position(5,4) },
                new [] { new Position(4,5) },
                new [] { new Position(6,5) },
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
            var movement = new HorizontalVerticalMovement(1);
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(0, 1)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 0)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 2)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(2, 1)).Should().BeTrue();
        }

        [Fact]
        public void KingIsMovementAllowed_IsFalse()
        {
            var movement = new HorizontalVerticalMovement(1);
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(2, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(3, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(3, 3)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 3)).Should().BeFalse();
        }
       
        [Fact]
        public void RookAllowedPosition()
        {
            var movement = new HorizontalVerticalMovement();
            var actualMovements = movement.GetAllowedPositions(new Position(1, 2)).ToArray();
            var expectedMovements = new[]
            {
                new[] 
                {
                    new Position(1,3),
                    new Position(1,4),
                    new Position(1,5),
                    new Position(1,6),
                    new Position(1,7)
                },
                new[]
                {
                    new Position(1,1),
                    new Position(1,0),
                },
                new[]
                {
                    new Position(0,2),
                },
                new[]
                {
                    new Position(2,2),
                    new Position(3,2),
                    new Position(4,2),
                    new Position(5,2),
                    new Position(6,2),
                    new Position(7,2),
                },
            };

            actualMovements.Should().HaveSameCount(expectedMovements);
            for (var i = 0; i < expectedMovements.Length; i++)
            {
                actualMovements[i].Should().HaveSameCount(expectedMovements[i]).And.Contain(expectedMovements[i]);
            }
        }

        [Fact]
        public void RookIsMovementAllowed_IsTrue()
        {
            var movement = new HorizontalVerticalMovement();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(0, 1)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 0)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 2)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(2, 1)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(1, 3)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(6, 1)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(1, 1), new Position(0, 1)).Should().BeTrue();
        }

        [Fact]
        public void RookIsMovementAllowed_IsFalse()
        {
            var movement = new HorizontalVerticalMovement();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(5, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(2, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(6, 6)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(5, 5), new Position(7, 7)).Should().BeFalse();
        }
    }
}
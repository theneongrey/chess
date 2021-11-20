using FluentAssertions;
using GameLogic.BasicMovements;
using Xunit;

namespace GameLogic.Test.BasicMovements
{
    public class PawnMovementTest
    {
        [Fact]
        public void PawnUpAtStartPositionAllowedPositions()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            var actualMovements = movement.GetAllowedPositions(new Position(1, 1));
            var expectedMovements = new[]
            {
                new Position(1,2),
                new Position(1,3)
            };

            actualMovements.Should().HaveSameCount(expectedMovements).And.Contain(expectedMovements);
        }

        [Fact]
        public void PawnUpNotAtStartPositionAllowedPositions()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            var actualMovements = movement.GetAllowedPositions(new Position(1, 3));
            var expectedMovements = new[]
            {
                new Position(1,4)
            };

            actualMovements.Should().HaveSameCount(expectedMovements).And.Contain(expectedMovements);
        }

        [Fact]
        public void PawnDownAtStartPositionAllowedPositions()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            var actualMovements = movement.GetAllowedPositions(new Position(1, 6));
            var expectedMovements = new[]
            {
                new Position(1,5),
                new Position(1,4)
            };

            actualMovements.Should().HaveSameCount(expectedMovements).And.Contain(expectedMovements);
        }

        [Fact]
        public void PawnDownNotAtStartPositionAllowedPositions()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            var actualMovements = movement.GetAllowedPositions(new Position(1, 3));
            var expectedMovements = new[]
            {
                new Position(1,2)
            };

            actualMovements.Should().HaveSameCount(expectedMovements).And.Contain(expectedMovements);
        }

        [Fact]
        public void PawnUpAtStartPointMovementAllowed_IsTrue()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(0, 2)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(0, 3)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 1), new Position(3, 2)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 1), new Position(3, 3)).Should().BeTrue();
        }

        [Fact]
        public void PawnUpAtStartPointMovementAllowed_IsFalse()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(0, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(0, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(1, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(1, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 1), new Position(2, 1)).Should().BeFalse();
        }

        [Fact]
        public void PawnUpNotAtStartPointMovementAllowed_IsTrue()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 3)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 3), new Position(3, 4)).Should().BeTrue();
        }

        [Fact]
        public void PawnUpNotAtStartPointMovementAllowed_IsFalse()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Up);
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(1, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(1, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(2, 1)).Should().BeFalse();
        }

        [Fact]
        public void PawnDownAtStartPointMovementAllowed_IsTrue()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(0, 5)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(0, 4)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 6), new Position(3, 5)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 6), new Position(3, 4)).Should().BeTrue();
        }

        [Fact]
        public void PawnDownAtStartPointMovementAllowed_IsFalse()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(0, 6)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(0, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(1, 5)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(1, 6)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 6), new Position(4, 6)).Should().BeFalse();
        }

        [Fact]
        public void PawnDownNotAtStartPointMovementAllowed_IsTrue()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 1)).Should().BeTrue();
            movement.IsTargetPositionAllowed(new Position(3, 3), new Position(3, 2)).Should().BeTrue();
        }

        [Fact]
        public void PawnDownNotAtStartPointMovementAllowed_IsFalse()
        {
            var movement = new DefaultPawnMovement(PawnDirection.Down);
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 0)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 3)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(0, 4)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(1, 2)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(1, 1)).Should().BeFalse();
            movement.IsTargetPositionAllowed(new Position(0, 2), new Position(1, 3)).Should().BeFalse();
        }
    }
}

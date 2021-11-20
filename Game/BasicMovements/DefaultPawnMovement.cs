namespace GameLogic.BasicMovements
{
    public enum PawnDirection { Up, Down };

    public class DefaultPawnMovement : IBasicMovement
    {
        private int _direction;

        public DefaultPawnMovement(PawnDirection direction)
        {
            _direction = direction == PawnDirection.Up ? 1 : -1;
        }

        private int GetStartY()
        {
            return _direction == 1 ? 1 : 6;
        }

        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition)
        {
            if (startPosition.Y == GetStartY())
            {
                return new[] {
                    new[]
                        {
                            new Position(startPosition.X, startPosition.Y + _direction),
                            new Position(startPosition.X, startPosition.Y + _direction + _direction)
                        }
                    };
            }
            else
            {
                return new[] { new[] { new Position(startPosition.X, startPosition.Y + _direction) } };
            }
        }

        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition)
        {
            return currentPosition.X == targetPosition.X && (targetPosition.Y - currentPosition.Y == _direction ||
                currentPosition.Y == GetStartY() && targetPosition.Y - currentPosition.Y == 2 * _direction);
        }
    }
}

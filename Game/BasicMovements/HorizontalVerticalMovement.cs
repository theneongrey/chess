namespace GameLogic.BasicMovements
{
    public class HorizontalVerticalMovement : IBasicMovement
    {
        private int _maxRange;

        public HorizontalVerticalMovement(int maxRange = 8)
        {
            _maxRange = maxRange;
        }

        public IEnumerable<Position> GetAllowedPositions(Position startPosition)
        {
            List<Position> allowedPositions = new List<Position>();
            for (var x = Math.Max(startPosition.X - _maxRange, 0); x <= Math.Min(startPosition.X + _maxRange, 7); x++)
            {
                if (startPosition.X == x)
                {
                    continue;
                }

                allowedPositions.Add(new Position(x, startPosition.Y));
            }
            for (var y = Math.Max(startPosition.Y - _maxRange, 0); y <= Math.Min(startPosition.Y + _maxRange, 7); y++)
            {
                if (startPosition.Y == y)
                {
                    continue;
                }

                allowedPositions.Add(new Position(startPosition.X, y));
            }

            return allowedPositions;
        }

        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition)
        {
            if (currentPosition == targetPosition)
            {
                return false;
            }

            return (Math.Abs(currentPosition.X - targetPosition.X) <= _maxRange && currentPosition.Y == targetPosition.Y ||
                Math.Abs(currentPosition.Y - targetPosition.Y) <= _maxRange && currentPosition.X == targetPosition.X);
        }
    }
}

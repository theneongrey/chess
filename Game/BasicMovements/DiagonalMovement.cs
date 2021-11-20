namespace GameLogic.BasicMovements
{
    public class DiagonalMovement : IBasicMovement
    {
        private int _maxRange;

        public DiagonalMovement(int maxRange = 8)
        {
            _maxRange = maxRange;
        }

        public IEnumerable<Position> GetAllowedPositions(Position startPosition)
        {
            List<Position> allowedPositions = new List<Position>();

            var x = startPosition.X - _maxRange - 1;
            var y = startPosition.Y - _maxRange - 1;

            do
            {
                ++x;
                ++y;
                if (x < 0 || y < 0 || x == startPosition.X)
                {
                    continue;
                }

                allowedPositions.Add(new Position(x, y));
            } while (x < 7 && y < 7 && x-startPosition.X < _maxRange);

            x = startPosition.X - _maxRange - 1;
            y = startPosition.Y + _maxRange + 1;
            do
            {
                ++x;
                --y;
                if (x < 0 || y > 7 || x == startPosition.X)
                {
                    continue;
                }

                allowedPositions.Add(new Position(x, y));
            } while (x < 7 && y > 0 && x - startPosition.X < _maxRange);

            return allowedPositions;
        }

        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition)
        {
            if (currentPosition == targetPosition)
            {
                return false;
            }

            var diffX = Math.Abs(currentPosition.X - targetPosition.X);
            var diffY = Math.Abs(currentPosition.Y - targetPosition.Y);

            return diffX == diffY && diffX <= _maxRange;
        }
    }
}

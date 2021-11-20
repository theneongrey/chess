namespace GameLogic.BasicMovements
{
    public class HorizontalVerticalMovement : IBasicMovement
    {
        private int _maxRange;

        public HorizontalVerticalMovement(int maxRange = 8)
        {
            _maxRange = maxRange;
        }

        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition)
        {
            var movesUp = new List<Position>();
            var movesDown = new List<Position>();
            var movesLeft = new List<Position>();
            var movesRight = new List<Position>();

            for (var i = 1; i <= _maxRange; i++)
            {
                var up = startPosition.Y + i;
                var down = startPosition.Y - i;
                var left = startPosition.X - i;
                var right = startPosition.X + i;

                if (up < 8)
                {
                    movesUp.Add(new Position(startPosition.X, up));
                }
                if (down >= 0)
                {
                    movesDown.Add(new Position(startPosition.X, down));
                }
                if (left >= 0)
                {
                    movesLeft.Add(new Position(left, startPosition.Y));
                }
                if (right < 8)
                {
                    movesRight.Add(new Position(right, startPosition.Y));
                }
            }

            return new[] { movesUp, movesDown, movesLeft, movesRight };
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

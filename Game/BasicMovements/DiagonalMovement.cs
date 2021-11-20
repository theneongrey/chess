namespace GameLogic.BasicMovements
{
    public class DiagonalMovement : IBasicMovement
    {
        private int _maxRange;

        public DiagonalMovement(int maxRange = 8)
        {
            _maxRange = maxRange;
        }

        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition)
        {
            var movesUpRight = new List<Position>();
            var movesUpLeft = new List<Position>();
            var movesDownRight = new List<Position>();
            var movesDownLeft = new List<Position>();
            for (var i = 1; i <= _maxRange; i++)
            {
                var rightX = startPosition.X + i;
                var leftX = startPosition.X - i;
                var upY = startPosition.Y + i;
                var downY = startPosition.Y - i;

                if (rightX < 8 && upY < 8)
                {
                    movesUpRight.Add(new Position(rightX, upY));
                }
                if (leftX >= 0 && upY < 8)
                {
                    movesUpLeft.Add(new Position(leftX, upY));
                }
                if (rightX < 8 && downY >= 0)
                {
                    movesDownRight.Add(new Position(rightX, downY));
                }
                if (leftX >= 0 && downY >= 0)
                {
                    movesDownLeft.Add(new Position(leftX, downY));
                }
            }

            return new[] { movesUpRight, movesUpLeft, movesDownRight, movesDownLeft };
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

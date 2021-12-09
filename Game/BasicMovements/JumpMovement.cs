namespace GameLogic.BasicMovements
{
    internal class JumpMovement : IBasicMovement
    {
        private (int, int)[] _allowedJumps = new[]
        {
            (1, 2),
            (-1, 2),
            (-1, -2),
            (1, -2),

            (2, 1),
            (-2, 1),
            (-2, -1),
            (2, -1)
        };

        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition)
        {
            var result = new List<Position[]>();
            
            foreach (var position in _allowedJumps)
            {
                var newX = position.Item1 + startPosition.X;
                var newY = position.Item2 + startPosition.Y;

                if (newX >= 0 && newY >= 0 && newX <= 7 && newY <= 7)
                {
                    result.Add(new[] { new Position(newX, newY) });
                }
            }

            return result;
        }

        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition)
        {
            var deltaX = Math.Abs(currentPosition.X - targetPosition.X);
            var deltaY = Math.Abs(currentPosition.Y - targetPosition.Y);

            return deltaX == 2 && deltaY == 1 || deltaX == 1 && deltaY == 2;
        }
    }
}

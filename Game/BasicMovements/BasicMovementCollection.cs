namespace GameLogic.BasicMovements
{
    public class BasicMovementCollection : IBasicMovement
    {
        private IBasicMovement[] _collections;

        public BasicMovementCollection(params IBasicMovement[] collections)
        {
            _collections = collections;
        }

        public IEnumerable<Position> GetAllowedPositions(Position startPosition)
        {
            var allowedMovement = new List<Position>();
            foreach (var movement in _collections)
            {
                allowedMovement.AddRange(movement.GetAllowedPositions(startPosition));
            }

            return allowedMovement;
        }

        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition)
        {
            foreach (var movement in _collections)
            {
                if (movement.IsTargetPositionAllowed(currentPosition, targetPosition))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

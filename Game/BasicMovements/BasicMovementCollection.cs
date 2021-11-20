namespace GameLogic.BasicMovements
{
    public class BasicMovementCollection : IBasicMovement
    {
        private IBasicMovement[] _collections;

        public BasicMovementCollection(params IBasicMovement[] collections)
        {
            _collections = collections;
        }

        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition)
        {
            var result = new List<IEnumerable<Position>>();
            foreach (var movementCollection in _collections)
            {
                foreach (var positionCollection in movementCollection.GetAllowedPositions(startPosition))
                {
                    result.Add(positionCollection);
                }
            }

            return result;
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

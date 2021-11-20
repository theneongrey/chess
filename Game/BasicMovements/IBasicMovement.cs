namespace GameLogic.BasicMovements
{
    public interface IBasicMovement
    {
        public IEnumerable<Position> GetAllowedPositions(Position startPosition);
        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition);
    }
}

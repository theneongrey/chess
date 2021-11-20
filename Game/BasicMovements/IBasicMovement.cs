namespace GameLogic.BasicMovements
{
    public interface IBasicMovement
    {
        /// <summary>
        /// Returns a collection of possible moves and their position
        /// Each move starts at the position of the piece.
        /// Each move in a direction is returned as an array of possible moves, so that when an piece in in the way of the move, the following moves can be ignored
        /// </summary>
        /// <param name="startPosition">Current position of the piece</param>
        /// <returns>Move/Position collection</returns>
        public IEnumerable<IEnumerable<Position>> GetAllowedPositions(Position startPosition);
        public bool IsTargetPositionAllowed(Position currentPosition, Position targetPosition);
    }
}

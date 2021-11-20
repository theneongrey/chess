using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class QueenPiece : APiece
    {
        private IBasicMovement _basicMovements;

        public QueenPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
        }

        protected override IEnumerable<Position> GetAllowedPositions(Field field)
        {
            return _basicMovements.GetAllowedPositions(Position);
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            return _basicMovements.IsTargetPositionAllowed(Position, targetPosition);
        }
    }
}

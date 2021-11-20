using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class BishopPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override char Identifier => 'B';

        public BishopPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new DiagonalMovement();
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field);
            return filteredPositions;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            return field.GetPieceAt(targetPosition)?.Color != Color && _basicMovements.IsTargetPositionAllowed(Position, targetPosition);
        }
    }
}

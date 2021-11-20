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

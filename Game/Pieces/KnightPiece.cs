using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class KnightPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override char Identifier => 'N';


        public KnightPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new JumpMovement();
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

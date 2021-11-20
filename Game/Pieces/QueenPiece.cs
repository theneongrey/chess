using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class QueenPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override char Identifier => 'Q';


        public QueenPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
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

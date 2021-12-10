using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class KnightPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override Piece PieceType { get; }

        public KnightPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new JumpMovement();
            PieceType = color == PieceColor.White ? ColoredPieces.WhiteKnight : ColoredPieces.BlackKnight;
        }
        public override object Clone() => Clone(new KnightPiece(Position, Color));

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

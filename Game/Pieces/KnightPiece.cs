using GameLogic.BasicMovements;

namespace GameLogic.InternPieces
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

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), board);
            return filteredPositions;
        }

        public override bool IsTargetPositionAllowed(Board board, Position targetPosition)
        {
            return board.GetPieceAt(targetPosition)?.Color != Color && _basicMovements.IsTargetPositionAllowed(Position, targetPosition);
        }
    }
}

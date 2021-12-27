using GameLogic.BasicMovements;

namespace GameLogic.InternPieces
{
    internal class RookPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override GamePiece PieceType { get; }

        public RookPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new HorizontalVerticalMovement();
            PieceType = color == PieceColor.White ? GamePieces.WhiteRook : GamePieces.BlackRook;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), board);
            var allowedPositions = new List<IEnumerable<Position>>(filteredPositions);

            return allowedPositions;
        }

        public override bool IsTargetPositionAllowed(Board board, Position targetPosition)
        {
            // check for general movement validity
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                return false;
            }

            // check for obstacles
            // the following calculation is to avoid writing special cases for different axises and directions
            var deltaX = targetPosition.X - Position.X; // delta to calc step
            var deltaY = targetPosition.Y - Position.Y;
            var absDeltaX = Math.Abs(deltaX);
            var absDeltaY = Math.Abs(deltaY);
            var stepX = deltaX == 0 ? 0 : deltaX / absDeltaX; // delta / Abs(delta) should return 1 or -1 (or 0 when 0)
            var stepY = deltaY == 0 ? 0 : deltaY / absDeltaY;

            for (var offset = 1; offset < Math.Max(absDeltaX, absDeltaY); offset++)
            {
                if (!board.IsCellEmpty(new Position(Position.X + stepX*offset, Position.Y + stepY * offset)))
                {
                    return false;
                }
            }

            // check if cell is taken or if casteling is possible
            return board.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

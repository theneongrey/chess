using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class RookPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override string Identifier => "R";

        public RookPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new HorizontalVerticalMovement();
        }

        private bool IsCastlingPossible(Field field)
        {
            var pieceAtKingPosition = field.GetPieceAt(new Position(4, Position.Y));
            if (pieceAtKingPosition is KingPiece king && !king.WasMoved)
            {
                var direction = king.Position.X < Position.X ? -1 : 1;
                for (var x = Position.X + direction; x != 4; x += direction)
                {
                    if (!field.IsCellEmpty(new Position(x, Position.Y)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field);
            var allowedPositions = new List<IEnumerable<Position>>(filteredPositions);
            if (!WasMoved && IsCastlingPossible(field))
            {
                allowedPositions.Add(new[] { new Position(4, Position.Y) });
            }

            return allowedPositions;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
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
                if (!field.IsCellEmpty(new Position(Position.X + stepX*offset, Position.Y + stepY * offset)))
                {
                    return false;
                }
            }

            // check if cell is taken or if casteling is possible
            return field.GetPieceAt(targetPosition)?.Color != Color || 
                (targetPosition.X == 4 && targetPosition.Y == Position.Y && IsCastlingPossible(field));
        }
    }
}

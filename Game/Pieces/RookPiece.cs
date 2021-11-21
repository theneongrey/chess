using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class RookPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override string Identifier => "R";

        public RookPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new HorizontalVerticalMovement(1);
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
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                return targetPosition.X == 4 && targetPosition.Y == Position.Y && IsCastlingPossible(field);
            }

            return field.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class RookPiece : APiece
    {
        private IBasicMovement _basicMovements;

        public RookPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new HorizontalVerticalMovement(1);
        }

        protected override IEnumerable<Position> GetAllowedPositions(Field field)
        {
            var allowedPositions = new List<Position>(_basicMovements.GetAllowedPositions(Position));
            if (!WasMoved)
            {
                var king = field.GetPiecesByTypeAndColor<KingPiece>(Color).First();
                if (!king.WasMoved)
                {
                    allowedPositions.Add(king.Position);
                }
            }

            return allowedPositions;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                var king = field.GetPiecesByTypeAndColor<KingPiece>(Color).First();
                if (!king.WasMoved && king.Position == targetPosition)
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}

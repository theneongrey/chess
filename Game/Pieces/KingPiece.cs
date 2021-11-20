using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class KingPiece : APiece
    {
        private IBasicMovement _basicMovements;

        public KingPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(1), new HorizontalVerticalMovement(1));
        }

        protected override IEnumerable<Position> GetAllowedPositions(Field field)
        {
            var allowedPositions = new List<Position>(_basicMovements.GetAllowedPositions(Position));
            if (!WasMoved)
            {
                var rooks = field.GetPiecesByTypeAndColor<RookPiece>(Color);
                foreach(var rook in rooks)
                {
                    if (!rook.WasMoved)
                    {
                        allowedPositions.Add(rook.Position);
                    }
                }
            }

            return allowedPositions;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                var rooks = field.GetPiecesByTypeAndColor<RookPiece>(Color);
                foreach (var rook in rooks)
                {
                    if (!rook.WasMoved && rook.Position == targetPosition)
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }
    }
}

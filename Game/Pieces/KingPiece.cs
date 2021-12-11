using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class KingPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override Piece PieceType { get; }

        public KingPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(1), new HorizontalVerticalMovement(1));
            PieceType = color == PieceColor.White ? ColoredPieces.WhiteKing : ColoredPieces.BlackKing;
        }

        private bool IsLeftCastlingPossible(Board field)
        {
            var row = Position.Y;
            var leftRook = field.GetPieceAt(new Position(0, row));

            if (leftRook is not RookPiece || leftRook.WasMoved)
            {
                return false;
            }

            for (var i = 1; i < 4; i++)
            {
                if (!field.IsCellEmpty(new Position(i, row)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsRightCastlingPossible(Board field)
        {
            var row = Position.Y;
            var leftRook = field.GetPieceAt(new Position(0, row));

            if (leftRook is not RookPiece || leftRook.WasMoved)
            {
                return false;
            }
            
            if (!field.IsCellEmpty(new Position(5, row)) || 
                !field.IsCellEmpty(new Position(6, row)))
            {
                return false;
            }

            return true;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field);
            var allowedPositions = new List<IEnumerable<Position>>(filteredPositions);
            if (!WasMoved)
            {
                if (IsLeftCastlingPossible(field)) 
                { 
                    allowedPositions.Add(new[] { new Position(2, Position.Y) });
                }
                if (IsRightCastlingPossible(field))
                {
                    allowedPositions.Add(new[] { new Position(6, Position.Y) });
                }
            }

            return allowedPositions;
        }

        public override bool IsTargetPositionAllowed(Board field, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                if (!WasMoved && targetPosition.Y == Position.Y) 
                {
                    if (targetPosition.X == 2)
                    {
                        return IsLeftCastlingPossible(field);
                    }
                    if (targetPosition.X == 6)
                    {
                        return IsRightCastlingPossible(field);
                    }
                }

                return false;
            }

            return field.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

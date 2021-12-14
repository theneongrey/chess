using GameLogic.BasicMovements;

namespace GameLogic.InternPieces
{
    internal class KingPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override GamePiece PieceType { get; }

        public KingPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(1), new HorizontalVerticalMovement(1));
            PieceType = color == PieceColor.White ? GamePieces.WhiteKing : GamePieces.BlackKing;
        }

        private bool IsLeftCastlingPossible(Board board)
        {
            var row = Position.Y;
            var leftRook = board.GetPieceAt(new Position(0, row));

            if (leftRook is not RookPiece || leftRook.WasMoved)
            {
                return false;
            }

            for (var i = 1; i < 4; i++)
            {
                if (!board.IsCellEmpty(new Position(i, row)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsRightCastlingPossible(Board board)
        {
            var row = Position.Y;
            var leftRook = board.GetPieceAt(new Position(0, row));

            if (leftRook is not RookPiece || leftRook.WasMoved)
            {
                return false;
            }
            
            if (!board.IsCellEmpty(new Position(5, row)) || 
                !board.IsCellEmpty(new Position(6, row)))
            {
                return false;
            }

            return true;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), board);
            var allowedPositions = new List<IEnumerable<Position>>(filteredPositions);
            if (!WasMoved)
            {
                if (IsLeftCastlingPossible(board)) 
                { 
                    allowedPositions.Add(new[] { new Position(2, Position.Y) });
                }
                if (IsRightCastlingPossible(board))
                {
                    allowedPositions.Add(new[] { new Position(6, Position.Y) });
                }
            }

            return allowedPositions;
        }

        public override bool IsTargetPositionAllowed(Board board, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                if (!WasMoved && targetPosition.Y == Position.Y) 
                {
                    if (targetPosition.X == 2)
                    {
                        return IsLeftCastlingPossible(board);
                    }
                    if (targetPosition.X == 6)
                    {
                        return IsRightCastlingPossible(board);
                    }
                }

                return false;
            }

            return board.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

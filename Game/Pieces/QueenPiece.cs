using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class QueenPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override Piece PieceType { get; }

        public QueenPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
            PieceType = color == PieceColor.White ? ColoredPieces.WhiteQueen : ColoredPieces.BlackQueen;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field);
            return filteredPositions;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            // check if target position is valid move
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                return false;
            }

            // check for obstacles
            // precalc values
            var deltaX = targetPosition.X - Position.X; 
            var deltaY = targetPosition.Y - Position.Y;
            var absDeltaX = Math.Abs(deltaX);
            var absDeltaY = Math.Abs(deltaY);
            var stepX = deltaX == 0 ? 0 : deltaX / absDeltaX; // delta / Abs(delta) should return 1 or -1 (or 0 when 0)
            var stepY = deltaY == 0 ? 0 : deltaY / absDeltaY;

            if (deltaX == deltaY) // diagonal check
            {
                // avoid check, if next cell would be target position
                if (Position.X + stepX != targetPosition.X)
                {
                    var y = Position.Y;
                    for (var x = Position.X + stepX; x != targetPosition.X; x += stepX)
                    {
                        y += stepY;
                        if (field.GetPieceAt(new Position(x, y)) != null)
                        {
                            return false;
                        }
                    }
                }
            }
            else // vertical/horizontal check
            {
                for (var offset = 1; offset < Math.Max(absDeltaX, absDeltaY); offset++)
                {
                    if (!field.IsCellEmpty(new Position(Position.X + stepX * offset, Position.Y + stepY * offset)))
                    {
                        return false;
                    }
                }
            }

            // check if cell is taken by a piece in the same team
            return field.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

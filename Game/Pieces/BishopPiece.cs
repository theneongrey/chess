using GameLogic.BasicMovements;

namespace GameLogic.InternPieces
{
    internal class BishopPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override GamePiece PieceType { get; }

        public BishopPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new DiagonalMovement();
            PieceType = color == PieceColor.White ? GamePieces.WhiteBishop : GamePieces.BlackBishop;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), board);
            return filteredPositions;
        }

        public override bool IsTargetPositionAllowed(Board board, Position targetPosition)
        {
            // check if target position is valid move
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                return false;
            }

            // check if there is a piece in the way to target position
            var dirX = targetPosition.X > Position.X ? 1 : -1;
            var dirY = targetPosition.Y > Position.Y ? 1 : -1;

            // avoid check, if next cell would be target position
            if (Position.X + dirX != targetPosition.X)
            {
                var y = Position.Y;
                for (var x = Position.X + dirX; x != targetPosition.X; x += dirX)
                {
                    y += dirY;
                    if (board.GetPieceAt(new Position(x, y)) != null)
                    {
                        return false;
                    }
                }
            }


            return board.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

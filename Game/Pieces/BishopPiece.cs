using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class BishopPiece : APiece
    {
        private IBasicMovement _basicMovements;
        public override string Identifier => "B";

        public BishopPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            _basicMovements = new DiagonalMovement();
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
                    if (field.GetPieceAt(new Position(x, y)) != null)
                    {
                        return false;
                    }
                }
            }


            return field.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

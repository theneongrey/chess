using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    internal class PawnPiece : APiece
    {
        private IBasicMovement _basicMovements;
        private int _movementDirection;
        public override char Identifier => 'P';


        public PawnPiece(Position startPosition, PieceColor color) : base(startPosition, color)
        {
            PawnDirection pawnDirection;
            if (color == PieceColor.White)
            {
                pawnDirection = PawnDirection.Up;
                _movementDirection = 1;
            }
            else
            {
                pawnDirection= PawnDirection.Down;
                _movementDirection = -1;
            }

            _basicMovements = new DefaultPawnMovement(pawnDirection);
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field);
            var result = new List<IEnumerable<Position>>(filteredPositions);
            var leftCornerPiece = field.GetPieceAt(new Position(Position.X - 1, Position.Y + _movementDirection));
            var rightCornerPiece = field.GetPieceAt(new Position(Position.X + 1, Position.Y + _movementDirection));
            if (leftCornerPiece != null && leftCornerPiece.Color != Color)
            {
                result.Add(new[] { leftCornerPiece.Position });
            }
            if (rightCornerPiece != null && rightCornerPiece.Color != Color)
            {
                result.Add(new[] { rightCornerPiece.Position });
            }

            //todo implement enPassant detection

            return result;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                var leftCornerPiece = field.GetPieceAt(new Position(Position.X - 1, Position.Y + _movementDirection));
                if (leftCornerPiece != null && leftCornerPiece.Color != Color)
                {
                    return true;
                }
                var rightCornerPiece = field.GetPieceAt(new Position(Position.X - 1, Position.Y + _movementDirection));
                if (rightCornerPiece != null && rightCornerPiece.Color != Color)
                {
                    return true;
                }

                return false;
            }

            return field.GetPieceAt(targetPosition)?.Color != Color;
        }
    }
}

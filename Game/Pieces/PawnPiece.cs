﻿using GameLogic.BasicMovements;

namespace GameLogic.Pieces
{
    public class PawnPiece : APiece
    {
        private IBasicMovement _basicMovements;
        private int _movementDirection;
        public override string Identifier => "P";

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

        private bool CanPerformEnPassant(Field field, int x)
        {
            if (!(_movementDirection == 1 && Position.Y == 4 ||
                _movementDirection == -1 && Position.Y == 3))
            {
                return false;
            }

            var piece = field.GetPieceAt(new Position(x, Position.Y));
            return piece is PawnPiece pawn &&
                field.GetLastMovedPiece() == piece &&
                pawn.Color != Color &&
                pawn.LastPosition == new Position(x, Position.Y + _movementDirection * 2);
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), field, false);
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
            if (Position.X > 0 && CanPerformEnPassant(field, Position.X - 1))
            {
                result.Add(new[] { new Position(Position.X - 1, Position.Y + _movementDirection) });
            }
            if (Position.X < 7 && CanPerformEnPassant(field, Position.X + 1))
            {
                result.Add(new[] { new Position(Position.X + 1, Position.Y + _movementDirection) });
            }

            return result;
        }

        protected override bool IsTargetPositionAllowed(Field field, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                // check diagonal capture moves
                var leftCapturePosition = new Position(Position.X - 1, Position.Y + _movementDirection);
                var rightCapturePosition = new Position(Position.X + 1, Position.Y + _movementDirection);
                var leftCapturePiece = field.GetPieceAt(leftCapturePosition);
                var rightCapturePiece = field.GetPieceAt(rightCapturePosition);
                if (leftCapturePiece != null && leftCapturePiece.Color != Color && leftCapturePosition == targetPosition)
                {
                    return true;
                }
                if (rightCapturePiece != null && rightCapturePiece.Color != Color && rightCapturePosition == targetPosition)
                {
                    return true;
                }
                if (targetPosition.X == Position.X - 1 && targetPosition.Y == Position.Y + _movementDirection && 
                    CanPerformEnPassant(field, Position.X - 1))
                {
                    return true;
                }
                if (targetPosition.X == Position.X + 1 && targetPosition.Y == Position.Y + _movementDirection && 
                    CanPerformEnPassant(field, Position.X + 1))
                {
                    return true;
                }

                return false;
            }

            // on first move, jump to cells, check cell inbetween
            if (LastPosition == Position && Position.Y + _movementDirection != targetPosition.Y)
            {
                if (field.GetPieceAt(new Position(Position.X, Position.Y + _movementDirection)) != null)
                {
                    return false;
                }
            }

            return field.GetPieceAt(targetPosition) == null;
        }
    }
}

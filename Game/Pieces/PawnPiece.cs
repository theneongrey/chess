using GameLogic.BasicMovements;

namespace GameLogic.InternPieces
{
    internal class PawnPiece : APiece
    {
        private IBasicMovement _basicMovements;
        private int _movementDirection;
        public override Piece PieceType { get; }
        public bool AdvancedTwoCellsOnLastMove { get; private set; }

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
            PieceType = color == PieceColor.White ? ColoredPieces.WhitePawn : ColoredPieces.BlackPawn;
        }

        public override void Move(Position targetPosition)
        {
            AdvancedTwoCellsOnLastMove = Math.Abs(targetPosition.Y - Position.Y) == 2;
            base.Move(targetPosition);
        }

        private bool CanPerformEnPassant(Board board, int x)
        {
            if (!(_movementDirection == 1 && Position.Y == 4 ||
                _movementDirection == -1 && Position.Y == 3))
            {
                return false;
            }

            var piece = board.GetPieceAt(new Position(x, Position.Y));
            return piece is PawnPiece pawn &&
                board.LastMovedPiece == piece &&
                pawn.Color != Color &&
                pawn.AdvancedTwoCellsOnLastMove;
        }

        protected override IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board)
        {
            var filteredPositions = FilterMovementForObstacles(_basicMovements.GetAllowedPositions(Position), board, false);
            var result = new List<IEnumerable<Position>>(filteredPositions);

            var leftCornerPiece = board.GetPieceAt(new Position(Position.X - 1, Position.Y + _movementDirection));
            var rightCornerPiece = board.GetPieceAt(new Position(Position.X + 1, Position.Y + _movementDirection));
            if (leftCornerPiece != null && leftCornerPiece.Color != Color)
            {
                result.Add(new[] { leftCornerPiece.Position });
            }
            if (rightCornerPiece != null && rightCornerPiece.Color != Color)
            {
                result.Add(new[] { rightCornerPiece.Position });
            }
            if (Position.X > 0 && CanPerformEnPassant(board, Position.X - 1))
            {
                result.Add(new[] { new Position(Position.X - 1, Position.Y + _movementDirection) });
            }
            if (Position.X < 7 && CanPerformEnPassant(board, Position.X + 1))
            {
                result.Add(new[] { new Position(Position.X + 1, Position.Y + _movementDirection) });
            }

            return result;
        }

        public override bool IsTargetPositionAllowed(Board board, Position targetPosition)
        {
            if (!_basicMovements.IsTargetPositionAllowed(Position, targetPosition))
            {
                // check diagonal capture moves
                if (Position.X != 0)
                {
                    var leftCapturePosition = new Position(Position.X - 1, Position.Y + _movementDirection);
                    var leftCapturePiece = board.GetPieceAt(leftCapturePosition);

                    if (leftCapturePiece != null && leftCapturePiece.Color != Color && leftCapturePosition == targetPosition)
                    {
                        return true;
                    }

                    if (targetPosition.X == Position.X - 1 && targetPosition.Y == Position.Y + _movementDirection &&
                        CanPerformEnPassant(board, Position.X - 1))
                    {
                        return true;
                    }
                }

                if (Position.X != 7)
                {
                    var rightCapturePosition = new Position(Position.X + 1, Position.Y + _movementDirection);
                    var rightCapturePiece = board.GetPieceAt(rightCapturePosition);

                    if (rightCapturePiece != null && rightCapturePiece.Color != Color && rightCapturePosition == targetPosition)
                    {
                        return true;
                    }

                    if (targetPosition.X == Position.X + 1 && targetPosition.Y == Position.Y + _movementDirection &&
                        CanPerformEnPassant(board, Position.X + 1))
                    {
                        return true;
                    }
                }

                return false;
            }

            // on first move, jump to cells, check cell inbetween
            if (!WasMoved && Position.Y + _movementDirection != targetPosition.Y)
            {
                if (board.GetPieceAt(new Position(Position.X, Position.Y + _movementDirection)) != null)
                {
                    return false;
                }
            }

            return board.GetPieceAt(targetPosition) == null;
        }
    }
}

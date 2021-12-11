using GameLogic.CheckTester;

namespace GameLogic.InternPieces
{
    internal abstract class APiece
    {
        public Position Position { get; private set; }
        public bool WasMoved { get; private set; }
        public PieceColor Color { get; private set; }
        public abstract Piece PieceType { get; }

        protected APiece(Position startPosition, PieceColor color)
        {
            Position = startPosition;
            Color = color;
        }

        protected IEnumerable<IEnumerable<Position>> FilterMovementForObstacles(IEnumerable<IEnumerable<Position>> movements, Board board, bool canCaptureEnemyOnCell = true)
        {
            var result = new List<List<Position>>();
            foreach (var positionCollection in movements)
            {
                var filteredCollection = new List<Position>();
                foreach (var position in positionCollection)
                {
                    var piece = board.GetPieceAt(position);
                    if (piece != null)
                    {
                        if (piece.Color == Color || !canCaptureEnemyOnCell)
                        {
                            break;
                        }
                        else
                        {
                            filteredCollection.Add(position);
                            break;
                        }
                    }
                   filteredCollection.Add(position);
                }
                result.Add(filteredCollection);
            }

            return result;
        }

        protected IEnumerable<Position> CollectAllowedPositions(IEnumerable<IEnumerable<Position>> positions)
        {
            var result = new List<Position>();
            foreach (var positionCollection in positions)
            {
                foreach (var position in positionCollection)
                {
                    result.Add(position);
                }
            }

            return result;
        }

        protected abstract IEnumerable<IEnumerable<Position>> GetAllowedPositions(Board board);

        /// <summary>
        /// Checks if moving to target position is allowed. Avoids checking for kings threads
        /// </summary>
        /// <param name="board">Current board</param>
        /// <param name="targetPosition">Wished target position</param>
        /// <returns>Is target position allowed</returns>
        public abstract bool IsTargetPositionAllowed(Board board, Position targetPosition);

        public IEnumerable<Position> GetAllowedMoves(Board board)
        {
            var nonCheckTestAllowedMoves = CollectAllowedPositions(GetAllowedPositions(board));
            return nonCheckTestAllowedMoves.Where(p => !CheckTest.WillKingBeInDanger(board, this, p));
        }

        public bool CanMove(Board board)
        {
            var nonCheckTestAllowedMoves = CollectAllowedPositions(GetAllowedPositions(board));
            return nonCheckTestAllowedMoves.Any(p => !CheckTest.WillKingBeInDanger(board, this, p));
        }

        /// <summary>
        /// Checks if moving to target position is allowed, including check for kings safety
        /// </summary>
        /// <param name="board">Current board</param>
        /// <param name="targetPosition">Wished target position</param>
        /// <returns>Is move allowed</returns>
        public bool IsMoveAllowed(Board board, Position targetPosition)
        {
            return IsTargetPositionAllowed(board, targetPosition) && !CheckTest.WillKingBeInDanger(board, this, targetPosition);
        }

        public virtual void Move(Position targetPosition)
        {
            WasMoved = true;
            Position = targetPosition;
        }

        public void SetPosition(Position targetPosition)
        {
            Position = targetPosition;
        }

        public override string ToString()
        {
            return PieceType.Identifier;
        }
    }
}

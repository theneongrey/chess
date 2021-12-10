using GameLogic.CheckTester;

namespace GameLogic.Pieces
{
    internal abstract class APiece : ICloneable
    {
        public Position LastPosition { get; private set; }
        public Position Position { get; private set; }
        public bool WasMoved { get; private set; }
        public PieceColor Color { get; private set; }
        public abstract Piece PieceType { get; }

        protected APiece(Position startPosition, PieceColor color)
        {
            Position = startPosition;
            LastPosition = startPosition;
            Color = color;
        }

        protected APiece Clone(APiece newInstance)
        {
            newInstance.LastPosition = LastPosition;
            newInstance.WasMoved = WasMoved;
            
            return newInstance;
        }

        public abstract object Clone();

        protected IEnumerable<IEnumerable<Position>> FilterMovementForObstacles(IEnumerable<IEnumerable<Position>> movements, Field field, bool canCaptureEnemyField = true)
        {
            var result = new List<List<Position>>();
            foreach (var positionCollection in movements)
            {
                var filteredCollection = new List<Position>();
                foreach (var position in positionCollection)
                {
                    var piece = field.GetPieceAt(position);
                    if (piece != null)
                    {
                        if (piece.Color == Color || !canCaptureEnemyField)
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

        protected abstract IEnumerable<IEnumerable<Position>> GetAllowedPositions(Field field);
        protected abstract bool IsTargetPositionAllowed(Field field, Position targetPosition);

        public IEnumerable<Position> GetAllowedMoves(Field field)
        {
            var nonCheckTestAllowedMoves = CollectAllowedPositions(GetAllowedPositions(field));
            return nonCheckTestAllowedMoves.Where(p => !CheckTest.WillKingBeInDanger(field, this, p));
        }

        public bool CanMove(Field field)
        {
            var nonCheckTestAllowedMoves = CollectAllowedPositions(GetAllowedPositions(field));
            return nonCheckTestAllowedMoves.Any(p => !CheckTest.WillKingBeInDanger(field, this, p));
        }

        public bool IsMoveAllowed(Field field, Position targetPosition)
        {
            return IsTargetPositionAllowed(field, targetPosition) && !CheckTest.WillKingBeInDanger(field, this, targetPosition);
        }

        public void Move(Position targetPosition)
        {
            WasMoved = true;
            LastPosition = Position;
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

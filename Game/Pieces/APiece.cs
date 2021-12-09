﻿namespace GameLogic.Pieces
{
    internal abstract class APiece
    {
        public Position LastPosition { get; private set; }
        public Position Position { get; private set; }
        public bool WasMoved { get; private set; }
        public PieceColor Color { get; private set; }
        public abstract string Identifier { get; }

        protected APiece(Position startPosition, PieceColor color)
        {
            Position = startPosition;
            LastPosition = startPosition;
            Color = color;
        }

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
            return CollectAllowedPositions(GetAllowedPositions(field));
        }

        public bool IsMoveAllowed(Field field, Position targetPosition)
        {
            return IsTargetPositionAllowed(field, targetPosition);
        }

        public void Move(Position targetPosition)
        {
            WasMoved = true;
            LastPosition = Position;
            Position = targetPosition;
        }

        public override string ToString()
        {
            return (Color == PieceColor.White ? Identifier.ToLower() : Identifier.ToUpper());
        }
    }
}

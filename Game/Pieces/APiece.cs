namespace GameLogic.Pieces
{
    public abstract class APiece
    {
        public Position Position { get; private set; }
        public bool WasMoved { get; private set; }
        public PieceColor Color { get; private set; }

        protected APiece(Position startPosition, PieceColor color)
        {
            Position = startPosition;
        }

        protected IEnumerable<Position> FilterAllowedMovements(IEnumerable<Position> positions, Field field)
        {
            var result = new List<Position>();
            foreach (var position in positions)
            {
                var piece = field.GetPieceAt(position);
                if (piece == null || piece.Color != Color)
                {
                    result.Add(position);
                }
            }

            return result;
        }

        protected abstract IEnumerable<Position> GetAllowedPositions(Field field);
        protected abstract bool IsTargetPositionAllowed(Field field, Position targetPosition);

        public IEnumerable<Position> GetAllowedMoves(Field field)
        {
            return FilterAllowedMovements(GetAllowedPositions(field), field);
        }
        public bool IsMoveAllowed(Field field, Position targetPosition)
        {
            throw new NotImplementedException();
        }
    }
}

using GameLogic.Pieces;

namespace GameLogic 
{ 
    public class Field
    {
        private List<APiece> _allPieces;
        private List<APiece> _blackPieces;
        private List<APiece> _whitePieces;
        private APiece[] _cells;

        public Field()
        {
            _blackPieces = new List<APiece>();
            _whitePieces = new List<APiece>();
            _allPieces = new List<APiece>();
        }

        private void InitField()
        {

        }

        private void AddPiece(APiece piece)
        {
            var coloredList = GetPieceListByColor(piece.Color);
            coloredList.Add(piece);
            _allPieces.Add(piece);
            _cells[GetCellIndexByPosition(piece.Position)] = piece;
        }

        private List<APiece> GetPieceListByColor(PieceColor color)
        {
            return color == PieceColor.White ? _whitePieces : _blackPieces;
        }

        private int GetCellIndexByPosition(Position position)
        {
            return position.X + position.Y * 8;
        }

        internal IEnumerable<APiece> GetPiecesByTypeAndColor<T>(PieceColor color) where T : APiece
        {
            return GetPieceListByColor(color).OfType<T>();
        }

        /// <summary>
        /// Gets the content of a cell (a piece or null) at a given position. If the position is out of boundary, it will return null 
        /// </summary>
        /// <param name="position">Cell location</param>
        /// <returns>A pice or null</returns>
        internal APiece? GetPieceAt(Position position)
        {
            if (position.X < 0 || position.X > 7 || position.Y < 0 || position.Y > 7)
            {
                return null;
            }

            return _cells[GetCellIndexByPosition(position)];
        }
    }
}

using GameLogic.Pieces;
using System.Text;

namespace GameLogic 
{ 
    public class Field
    {
        private List<APiece> _allPieces;
        private List<APiece> _blackPieces;
        private List<APiece> _whitePieces;
        private APiece[] _cells;

        internal Field()
        {
            _blackPieces = new List<APiece>();
            _whitePieces = new List<APiece>();
            _allPieces = new List<APiece>();
            _cells = new APiece[64];
        }

        internal bool AddPiece(APiece piece)
        {
            var cellIndex = GetCellIndexByPosition(piece.Position);

            if (_cells[cellIndex] != null)
            {
                return false;
            }

            _cells[cellIndex] = piece;
            var coloredList = GetPieceListByColor(piece.Color);
            coloredList.Add(piece);
            _allPieces.Add(piece);

            return true;
        }

        private List<APiece> GetPieceListByColor(PieceColor color)
        {
            return color == PieceColor.White ? _whitePieces : _blackPieces;
        }

        private int GetCellIndexByPosition(Position position)
        {
            return position.X + position.Y * 8;
        }

        internal bool IsCellEmpty(Position position)
        {
            return GetPieceAt(position) == null;
        }

        /// <summary>
        /// Gets the content of a cell (a piece or null) at a given position. If the position is out of boundary, it will return null 
        /// </summary>
        /// <param name="position">Cell location</param>
        /// <returns>A pice or null</returns>
        public APiece? GetPieceAt(Position position)
        {
            if (position.X < 0 || position.X > 7 || position.Y < 0 || position.Y > 7)
            {
                return null;
            }

            return _cells[GetCellIndexByPosition(position)];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (var y = 7; y >= 0; y--)
            {
                for (var x = 0; x < 8; x++)
                {
                    var cell = y * 8 + x;
                    var piece = _cells[cell];

                    stringBuilder.Append(piece == null ? "  " : piece.ToString());
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}

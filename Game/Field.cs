using GameLogic.Pieces;
using System.Text;

namespace GameLogic 
{ 
    internal class Field
    {
        private List<APiece> _allPieces;
        private List<APiece> _blackPieces;
        private List<APiece> _whitePieces;
        private APiece?[] _cells;
        private APiece? _lastMovedPiece;

        internal Field()
        {
            _blackPieces = new List<APiece>();
            _whitePieces = new List<APiece>();
            _allPieces = new List<APiece>();
            _cells = new APiece[64];
        }

        internal bool AddPiece(APiece piece)
        {
            if (_cells[piece.Position.AsCellIndex] != null)
            {
                return false;
            }

            _cells[piece.Position.AsCellIndex] = piece;
            var coloredList = GetPieceListByColor(piece.Color);
            coloredList.Add(piece);
            _allPieces.Add(piece);

            return true;
        }

        private List<APiece> GetPieceListByColor(PieceColor color)
        {
            return color == PieceColor.White ? _whitePieces : _blackPieces;
        }

        public void RemovePiece(APiece? piece)
        {
            if (piece != null)
            {
                _cells[piece.Position.AsCellIndex] = null;

                if (piece.Color == PieceColor.White)
                {
                    _whitePieces.Remove(piece);
                }
                else
                {
                    _blackPieces.Remove(piece);
                }
                _allPieces.Remove(piece);
            }
        }

        public APiece? GetLastMovedPiece()
        {
            return _lastMovedPiece;
        }

        internal bool IsCellEmpty(Position position)
        {
            return GetPieceAt(position) == null;
        }

        /// <summary>
        /// Gets the content of a cell (a piece or null) at a given position. 
        /// </summary>
        /// <param name="position">Cell location</param>
        /// <returns>A pice or null</returns>
        public APiece? GetPieceAt(Position position)
        {
            return _cells[position.AsCellIndex];
        }
        
        public void MovePiece(APiece piece, Position to)
        {
            _lastMovedPiece = piece;

            var oldPos = piece.Position;
            piece.Move(to);

            _cells[oldPos.AsCellIndex] = null;
            _cells[to.AsCellIndex] = piece;
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

                    stringBuilder.Append(piece == null ? "-" : piece.ToString());
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}

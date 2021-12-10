using GameLogic.Pieces;
using System.Text;

namespace GameLogic 
{ 
    internal class Field
    {
        private APiece?[] _cells;
        public APiece? LastMovedPiece { get; private set; }

        internal Field()
        {
            _cells = new APiece[64];
        }

        internal void AddPiece(APiece piece)
        {
            _cells[piece.Position.AsCellIndex] = piece;
        }

        public void RemovePiece(APiece piece)
        {
            _cells[piece.Position.AsCellIndex] = null;
        }

        public IEnumerable<T> GetPiecesByTypeAndColor<T>(PieceColor color) where T : APiece
        {
            return _cells.Where(p => p is T && p.Color == color).Select(p => (T)p!);
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
        
        public void MovePieceTo(APiece piece, Position to)
        {
            LastMovedPiece = piece;
            RemovePiece(piece);
            piece.Move(to);
            AddPiece(piece);
        }

        public void SetPieceAt(APiece piece, Position at)
        {
            RemovePiece(piece);
            piece.SetPosition(at);
            AddPiece(piece);
        }

        public IEnumerable<APiece> GetPiecesByColor(PieceColor color)
        {
            return _cells.Where(p => p != null && p.Color == color)!;
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

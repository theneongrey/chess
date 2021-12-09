using GameLogic.Pieces;
using System.Text;

namespace GameLogic 
{ 
    internal class Field : ICloneable
    {
        public APiece?[] Cells { get; }
        public APiece? LastMovedPiece { get; private set; }

        internal Field()
        {
            Cells = new APiece[64];
        }

        public object Clone()
        {
            var clone = new Field();
            foreach (var cell in Cells)
            {
                if (cell != null)
                {
                    clone.AddPiece(cell);
                }
            }
            clone.LastMovedPiece = LastMovedPiece;
            return clone;
        }

        internal void AddPiece(APiece piece)
        {
            Cells[piece.Position.AsCellIndex] = piece;
        }

        public void RemovePiece(APiece piece)
        {
            Cells[piece.Position.AsCellIndex] = null;
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
            return Cells[position.AsCellIndex];
        }
        
        public void MovePiece(APiece piece, Position to)
        {
            LastMovedPiece = piece;

            RemovePiece(piece);
            piece.Move(to);
            AddPiece(piece);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (var y = 7; y >= 0; y--)
            {
                for (var x = 0; x < 8; x++)
                {
                    var cell = y * 8 + x;
                    var piece = Cells[cell];

                    stringBuilder.Append(piece == null ? "-" : piece.ToString());
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}

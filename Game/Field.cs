using GameLogic.Pieces;
using System.Text;

namespace GameLogic 
{ 
    public class Field
    {
        private List<APiece> _allPieces;
        private List<APiece> _blackPieces;
        private List<APiece> _whitePieces;
        private APiece?[] _cells;
        private APiece? _lastMovedPiece;

        public APiece? PieceCapturedOnLastMove { get; private set; }

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

        private APiece? GetCapturedPiece(APiece piece, Position to)
        {
            // not a pawn, not the enpassant special case
            if (piece is not PawnPiece pawn)
            {
                return GetPieceAt(to);
            }

            // moved forward, nothing was captured
            if (pawn.LastPosition.X - pawn.Position.X == 0)
            {
                return null;
            }

            // endposition makes sense for an enpassant and targetpositon is empty
            var pawnMovementDirection = pawn.Color == PieceColor.White ? 1 : -1;
            if (pawnMovementDirection == 1 && pawn.Position.Y == 5 ||
                pawnMovementDirection == -1 && pawn.Position.Y == 2 &&
                GetPieceAt(to) == null)
            {
                return GetPieceAt(new Position(to.X, to.Y - pawnMovementDirection));
            }

            return GetPieceAt(to);
        }

        private void RemovePieceFromLists(APiece? piece)
        {
            if (piece != null)
            {
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

        public bool IsCheckMate()
        {
            //ToDo
            return false;
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

                    stringBuilder.Append(piece == null ? "-" : piece.ToString());
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public bool MovePiece(Position from, Position to)
        {
            var piece = GetPieceAt(from);
            if (piece != null)
            {
                return MovePiece(piece, to);
            }

            return false;
        }

        public void ReplacePiece(APiece selectedPiece, APiece piece)
        {
            RemovePieceFromLists(selectedPiece);
            AddPiece(piece);
        }

        public bool MovePiece(APiece piece, Position to)
        {
            if (piece.Move(this, to))
            {
                var fromIndex = GetCellIndexByPosition(piece.LastPosition);
                var toCellIndex = GetCellIndexByPosition(to);

                _lastMovedPiece = piece;
                _cells[fromIndex] = null;

                APiece? capturedPiece = GetCapturedPiece(piece, to);
                PieceCapturedOnLastMove = capturedPiece;
                RemovePieceFromLists(capturedPiece);

                _cells[toCellIndex] = piece;

                return true;
            }

            return false;
        }
    }
}

using GameLogic;

namespace BlazorWAChess.ViewModel
{
    public class CellVM
    {
        private GamePiece? _piece;
        public string Icon { get; private set; } = string.Empty;
        public string PieceClass { get; private set; } = "blackPiece";
        public string CellClass { get; private set; } = "";
        public bool IsSelected { get; private set; }
        public bool IsHighlighted { get; private set; }
        public int X { get; }
        public int Y { get; }

        public CellVM(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(GamePiece? piece)
        {
            _piece = piece;

            if (_piece == null)
            {
                Icon = string.Empty;
                PieceClass = "blackPiece";
            }
            else
            {
                Icon = _piece.Identifier switch
                {
                    GamePieceIdentifier.King => _piece.IsWhite ? "♔" : "♚",
                    GamePieceIdentifier.Queen => _piece.IsWhite ? "♕" : "♛",
                    GamePieceIdentifier.Rook => _piece.IsWhite ? "♖" : "♜",
                    GamePieceIdentifier.Knight => _piece.IsWhite ? "♘" : "♞",
                    GamePieceIdentifier.Bishop => _piece.IsWhite ? "♗" : "♝",
                    GamePieceIdentifier.Pawn => _piece.IsWhite ? "♙" : "♟",
                    _ => throw new NotImplementedException(),
                };
                PieceClass = _piece.IsWhite ? "whitePiece" : "blackPiece";
            }
        }

        private void AdaptCellClass()
        {
            if (IsSelected)
            {
                CellClass = "selectedCell";
            }
            else if (IsHighlighted)
            {
                CellClass = "highlightedCell";
            }
            else
            {
                CellClass = string.Empty;
            }
        }

        public void Select()
        {
            IsSelected = true;
            AdaptCellClass();
        }

        public void Deselect()
        {
            IsSelected = false;
            AdaptCellClass();
        }

        internal void ToggleSelected()
        {
            IsSelected = !IsSelected;
            AdaptCellClass();
        }

        public void Highlight()
        {
            IsHighlighted = true;
            AdaptCellClass();
        }

        public void UnHighlight()
        {
            IsHighlighted = false;
            AdaptCellClass();
        }

    }
}

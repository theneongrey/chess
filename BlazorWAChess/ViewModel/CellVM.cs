using GameLogic;

namespace BlazorWAChess.ViewModel
{
    public class CellVM
    {
        private Piece? _piece;

        internal void Update(Piece? piece)
        {
            _piece = piece;
        }

        public override string ToString()
        {
            return _piece?.Identifier ?? string.Empty;
        }
    }
}

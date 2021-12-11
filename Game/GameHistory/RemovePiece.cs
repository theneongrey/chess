using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class RemovePiece : AGameMove
    {
        private APiece _piece;

        internal RemovePiece(APiece piece)
        {
            _piece = piece;
        }

        internal override void Redo(Board field)
        {
            field.RemovePiece(_piece);
        }

        internal override void Undo(Board field)
        {
            field.AddPiece(_piece);
        }
    }
}

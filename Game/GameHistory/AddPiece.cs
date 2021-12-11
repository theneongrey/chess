using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class AddPiece : AGameMove
    {
        private APiece _piece;

        internal AddPiece(APiece piece)
        {
            _piece = piece;
        }

        internal override void Redo(Board field)
        {
            field.AddPiece(_piece);
        }

        internal override void Undo(Board field)
        {
            field.RemovePiece(_piece);
        }
    }
}

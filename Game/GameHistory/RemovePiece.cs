using GameLogic.Pieces;

namespace GameLogic.GameHistory
{
    internal class RemovePiece : AGameMove
    {
        private APiece _piece;

        internal RemovePiece(APiece piece)
        {
            _piece = piece;
        }

        internal override void Redo(Field field)
        {
            field.RemovePiece(_piece);
        }

        internal override void Undo(Field field)
        {
            field.AddPiece(_piece);
        }
    }
}

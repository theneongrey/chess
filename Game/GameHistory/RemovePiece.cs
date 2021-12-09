using GameLogic.Pieces;

namespace GameLogic.GameHistory
{
    internal class RemovePiece : AGameMove
    {
        private APiece _piece;

        internal RemovePiece(Field field, APiece piece)
        {
            _piece = piece;
            Redo(field);
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

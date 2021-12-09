using GameLogic.Pieces;

namespace GameLogic.GameHistory
{
    internal class AddPiece : AGameMove
    {
        private APiece _piece;

        internal AddPiece(Field field, APiece piece)
        {
            _piece = piece;
            Redo(field);
        }

        internal override void Redo(Field field)
        {
            field.AddPiece(_piece);
        }

        internal override void Undo(Field field)
        {
            field.RemovePiece(_piece);
        }
    }
}

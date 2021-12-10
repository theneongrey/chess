using GameLogic.Pieces;

namespace GameLogic.GameHistory
{
    internal class MovePiece : AGameMove
    {
        private Position _from;
        private Position _to;
        private APiece _piece;

        internal MovePiece(Field field, APiece piece, Position to)
        {
            _from = piece.Position;
            _to = to;
            _piece = piece;
            Redo(field);
        }

        internal override void Redo(Field field)
        {
            field.MovePieceTo(_piece, _to);
        }

        internal override void Undo(Field field)
        {
            field.MovePieceTo(_piece, _from);
        }
    }
}

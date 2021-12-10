using GameLogic.Pieces;

namespace GameLogic.GameHistory
{
    internal class MovePiece : AGameMove
    {
        private Position _from;
        private Position _to;
        private APiece _piece;

        internal MovePiece(APiece piece, Position to)
        {
            _from = piece.Position;
            _to = to;
            _piece = piece;
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

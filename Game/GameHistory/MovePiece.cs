namespace GameLogic.GameHistory
{
    internal class MovePiece : AGameMove
    {
        private Position _from;
        private Position _to;

        internal MovePiece(Field field, Position from, Position to)
        {
            _from = from;
            _to = to;
            Redo(field);
        }

        internal override void Redo(Field field)
        {
            field.MovePiece(_from, _to);
        }

        internal override void Undo(Field field)
        {
            field.MovePiece(_to, _from);
        }
    }
}

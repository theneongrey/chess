namespace GameLogic.GameHistory
{
    internal class GameStack
    {
        private Stack<AGameMove> _moves;

        internal GameStack()
        {
            _moves = new Stack<AGameMove>();
        }

        internal void AddMove(AGameMove move)
        {
            _moves.Push(move);
        }

        internal void Undo(Field field)
        {
            if (_moves.Any())
            {
                var move = _moves.Pop();
                move.Undo(field);
            }
        }
    }
}

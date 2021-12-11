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

        internal void AddAndRunMove(Board board, AGameMove move)
        {
            _moves.Push(move);
            move.Redo(board);
        }

        internal void Undo(Board board)
        {
            if (_moves.Any())
            {
                var move = _moves.Pop();
                move.Undo(board);
            }
        }
    }
}

namespace GameLogic.GameHistory
{
    internal class GameStack
    {
        private int _currentMove;
        private List<AGameMove> _moves;
        public IReadOnlyList<AGameMove> Moves => _moves.ToArray();

        internal GameStack()
        {
            _moves = new List<AGameMove>();
        }

        internal void AddMove(AGameMove move)
        {
            if (_currentMove != _moves.Count)
            {
                for (var i = _currentMove; i < _moves.Count; i++)
                {
                    _moves.RemoveAt(_currentMove);
                }
            }

            _moves.Add(move);
            _currentMove = _moves.Count;
        }

        internal void AddAndRunMove(Board board, AGameMove move)
        {
            _moves.Add(move);
            move.Redo(board);
        }

        internal void Undo(Board board)
        {
            if (_moves.Any() && _currentMove > 0)
            {
                _currentMove--;
                var move = _moves[_currentMove];
                move.Undo(board);
            }
        }
        internal void Redo(Board board)
        {
            if (_moves.Any() && _currentMove < _moves.Count)
            {
                var move = _moves[_currentMove];
                move.Redo(board);
                _currentMove++;
            }
        }
    }
}

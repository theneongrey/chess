using GameLogic;

namespace BlazorWAChess.ViewModel
{
    public class MovesVM
    {
        private Game _game;
        public IEnumerable<string> Moves { get; private set; }

        public MovesVM(Game game)
        {
            _game = game;
            Moves = Array.Empty<string>();
        }

        public void Update()
        {
            Moves = _game.ToFullAlgebraicNotationArray();
        }
    }
}

using GameLogic;

namespace BlazorWAChess.ViewModel
{
    public class GameVM
    {
        public CellVM[][] Cells { get; private set; }

        public GameVM()
        {
            Cells = new CellVM[8][];
            CreateCells();
        }

        private void CreateCells()
        {
            for (var y = 0; y < Cells.Length; y++)
            {
                Cells[y] = new CellVM[8];
                for (var x = 0; x < Cells[y].Length; x++)
                {
                    Cells[y][x] = new CellVM();
                }
            }
        }

        public void Update(Game game)
        {
            UpdateCells(game);
        }

        private void UpdateCells(Game game)
        {
            var currentCells = game.GetBoard();
            for (var y = 0; y < currentCells.Length; y++)
            {
                for (var x = 0; x < currentCells[y].Length; x++)
                {
                    Cells[y][x].Update(currentCells[y][x]);
                }
            }
        }
    }
}

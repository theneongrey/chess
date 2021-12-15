﻿using GameLogic;

namespace BlazorWAChess.ViewModel
{
    public class GameVM
    {
        private Game _game;
        public CellVM[,] Cells { get; private set; }

        public GameVM(Game game)
        {
            _game = game;
            Cells = new CellVM[8,8];
            CreateCells();
        }

        private void CreateCells()
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    Cells[y,x] = new CellVM(x, y);
                }
            }
        }

        public void Update()
        {
            UpdateCells();
        }

        private CellVM? DeselectCells()
        {
            return SelectCell(-1, -1);
        }

        private CellVM? SelectCell(int x, int y)
        {
            CellVM? result = null;
            foreach (var cell in Cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    result = cell;
                    cell.ToggleSelected();
                }
                else
                {
                    cell.Deselect();
                    cell.UnHighlight();
                }
            }

            return result;
        }

        public bool CellOnClick(int x, int y)
        {
            var position = new Position(x, y);

            if (!_game.IsPieceSelected)
            {
                var selectedPiece = _game.SelectPiece(position);

                var selectedCell = SelectCell(x, y);
                
                if (selectedCell != null && selectedCell.IsSelected)
                {
                    var allowedMoves = _game.GetMovesForCell(position);
                    foreach (var move in allowedMoves)
                    {
                        Cells[move.Y, move.X].Highlight();
                    }
                }
            } 
            else
            {
                if (_game.TryMove(position))
                {
                    DeselectCells();
                    return true;
                }
            }

            return false;
        }

        private void UpdateCells()
        {
            var currentCells = _game.GetBoard();
            for (var y = 0; y < currentCells.Length; y++)
            {
                for (var x = 0; x < currentCells[y].Length; x++)
                {
                    Cells[y,x].Update(currentCells[y][x]);
                }
            }
        }
    }
}

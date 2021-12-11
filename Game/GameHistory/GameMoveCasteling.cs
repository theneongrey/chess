using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class GameMoveCasteling : AGameMove
    {
        private APiece _king;
        private Position _to;

        public GameMoveCasteling(APiece king, Position to)
        {
            _king = king;
            _to = to;
        }

        internal override void Redo(Board board)
        {
            APiece rook;
            if (_to.X > _king.Position.X)
            {
                rook = board.GetPieceAt(new Position(7, _king.Position.Y))!;
                board.MovePieceTo(_king, new Position(6, _king.Position.Y));
            }
            else
            {
                rook = board.GetPieceAt(new Position(0, _king.Position.Y))!;
                board.MovePieceTo(_king, new Position(2, _king.Position.Y));
            }
            board.MovePieceTo(rook, new Position(4, _king.Position.Y));
        }

        internal override void Undo(Board board)
        {
            APiece rook = board.GetPieceAt(new Position(4, _king.Position.Y))!;
            if (_king.Position.X > 4)
            {
                board.MovePieceTo(rook, new Position(7, _king.Position.Y));
            }
            else
            {
                board.MovePieceTo(rook, new Position(0, _king.Position.Y));
            }
            board.MovePieceTo(_king, new Position(4, _king.Position.Y));
        }
    }
}

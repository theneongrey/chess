using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class GameMoveMovePiece : AGameMove
    {
        private Position _from;
        private Position _to;
        private APiece _piece;

        internal GameMoveMovePiece(APiece piece, Position to)
        {
            _from = piece.Position;
            _to = to;
            _piece = piece;
        }

        internal override void Redo(Board board)
        {
            board.MovePieceTo(_piece, _to);
        }

        internal override void Undo(Board board)
        {
            board.MovePieceTo(_piece, _from);
        }
    }
}

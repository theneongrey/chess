using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class GameMoveRemovePiece : AGameMove
    {
        private APiece _piece;

        internal GameMoveRemovePiece(APiece piece)
        {
            _piece = piece;
        }

        internal override void Redo(Board board)
        {
            board.RemovePiece(_piece);
        }

        internal override void Undo(Board board)
        {
            board.AddPiece(_piece);
        }
    }
}

using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class PromotePiece : AGameMove
    {
        private APiece _pawn;
        private APiece _newPiece;

        public PromotePiece(APiece pawn, APiece newPiece)
        {
            _pawn = pawn;
            _newPiece = newPiece;
        }

        internal override void Redo(Board board)
        {
            board.RemovePiece(_pawn);
            board.AddPiece(_newPiece);
        }

        internal override void Undo(Board board)
        {
            board.RemovePiece(_newPiece);
            board.AddPiece(_pawn);
        }
    }
}

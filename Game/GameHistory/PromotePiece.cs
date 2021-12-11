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

        internal override void Redo(Board field)
        {
            field.RemovePiece(_pawn);
            field.AddPiece(_newPiece);
        }

        internal override void Undo(Board field)
        {
            field.RemovePiece(_newPiece);
            field.AddPiece(_pawn);
        }
    }
}

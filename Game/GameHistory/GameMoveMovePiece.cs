using GameLogic.InternPieces;

namespace GameLogic.GameHistory
{
    internal class GameMoveMovePiece : AGameMove
    {
        public Position From { get; private set; }
        public Position To { get; private set; }
        public string PieceIdentifier => _piece.PieceType.Identifier;
        private APiece _piece;

        internal GameMoveMovePiece(APiece piece, Position to)
        {
            From = piece.Position;
            To = to;
            _piece = piece;
        }

        internal override void Redo(Board board)
        {
            board.MovePieceTo(_piece, To);
        }

        internal override void Undo(Board board)
        {
            board.MovePieceTo(_piece, From);
        }
    }
}

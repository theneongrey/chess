using GameLogic;

namespace GameParser.PieceMapper
{
    public class EnglishPieceMapper : IPieceMapper
    {
        public string AllowedPromotionPieces => "QRBN";

        public string PawnName => "P";

        public Piece GetPieceByName(char name)
        {
            return name switch
            {
                'K' => Pieces.King,
                'Q' => Pieces.Queen,
                'R' => Pieces.Rook,
                'B' => Pieces.Bishop,
                'N' => Pieces.Knight,
                _ => Pieces.Pawn,
            };
        }
    }
}

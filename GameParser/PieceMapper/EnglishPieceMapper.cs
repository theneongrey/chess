using GameLogic;

namespace GameParser.PieceMapper
{
    public class EnglishPieceMapper : IPieceMapper
    {
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

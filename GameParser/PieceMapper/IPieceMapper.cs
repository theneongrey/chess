using GameLogic;

namespace GameParser.PieceMapper
{
    public interface IPieceMapper
    {
        public Piece GetPieceByName(char name);
        public string AllowedPromotionPieces { get; }
        public string PawnName { get; }
    }
}

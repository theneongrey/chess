namespace GameLogic
{
    public record Piece 
    {
        public string Identifier { get; }

        internal Piece(string identifier)
        {
            Identifier = identifier;
        }
    }

    public record GamePiece : Piece
    {
        public bool IsWhite { get; }
        public string ColoredIdentifier => IsWhite ? Identifier.ToLower() : Identifier;

        public GamePiece(string identifier, bool isWhite) : base(identifier)
        {
            IsWhite = isWhite;
        }
    }

    public class Pieces
    {
        public static Piece Queen { get; } = new Piece("Q");
        public static Piece Rook { get; } = new Piece("R");
        public static Piece Knight { get; } = new Piece("N");
        public static Piece Bishop { get; } = new Piece("B");
        public static Piece Pawn { get; } = new Piece("P");
        public static Piece King { get; } = new Piece("K");
    }

    public class TradablePieces
    {
        public static Piece Queen { get; } = Pieces.Queen;
        public static Piece Rook { get; } = Pieces.Rook;
        public static Piece Knight { get; } = Pieces.Knight;
        public static Piece Bishop { get; } = Pieces.Bishop;
    }

    public class GamePieces
    {
        public static GamePiece WhiteQueen { get; } = new GamePiece(Pieces.Queen.Identifier, true);
        public static GamePiece WhiteRook { get; } = new GamePiece(Pieces.Rook.Identifier, true);
        public static GamePiece WhiteKnight { get; } = new GamePiece(Pieces.Knight.Identifier, true);
        public static GamePiece WhiteBishop { get; } = new GamePiece(Pieces.Bishop.Identifier, true);
        public static GamePiece WhiteKing { get; } = new GamePiece(Pieces.King.Identifier, true);
        public static GamePiece WhitePawn { get; } = new GamePiece(Pieces.Pawn.Identifier, true);
        public static GamePiece BlackQueen { get; } = new GamePiece(Pieces.Queen.Identifier, false);
        public static GamePiece BlackRook { get; } = new GamePiece(Pieces.Rook.Identifier, false);
        public static GamePiece BlackKnight { get; } = new GamePiece(Pieces.Knight.Identifier, false);
        public static GamePiece BlackBishop { get; } = new GamePiece(Pieces.Bishop.Identifier, false);
        public static GamePiece BlackKing { get; } = new GamePiece(Pieces.King.Identifier, false);
        public static GamePiece BlackPawn { get; } = new GamePiece(Pieces.Pawn.Identifier, false);
    }

}

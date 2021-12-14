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

    public class GamePieceIdentifier
    {
        public const string Queen = "Q";
        public const string Rook = "R";
        public const string Knight = "N";
        public const string Bishop = "B";
        public const string Pawn = "P";
        public const string King = "K";
    }

    public class Pieces
    {
        public static Piece Queen { get; } = new Piece(GamePieceIdentifier.Queen);
        public static Piece Rook { get; } = new Piece(GamePieceIdentifier.Rook);
        public static Piece Knight { get; } = new Piece(GamePieceIdentifier.Knight);
        public static Piece Bishop { get; } = new Piece(GamePieceIdentifier.Bishop);
        public static Piece Pawn { get; } = new Piece(GamePieceIdentifier.Pawn);
        public static Piece King { get; } = new Piece(GamePieceIdentifier.King);
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
        public static GamePiece WhiteQueen { get; } = new GamePiece(GamePieceIdentifier.Queen, true);
        public static GamePiece WhiteRook { get; } = new GamePiece(GamePieceIdentifier.Rook, true);
        public static GamePiece WhiteKnight { get; } = new GamePiece(GamePieceIdentifier.Knight, true);
        public static GamePiece WhiteBishop { get; } = new GamePiece(GamePieceIdentifier.Bishop, true);
        public static GamePiece WhiteKing { get; } = new GamePiece(GamePieceIdentifier.King, true);
        public static GamePiece WhitePawn { get; } = new GamePiece(GamePieceIdentifier.Pawn, true);
        public static GamePiece BlackQueen { get; } = new GamePiece(GamePieceIdentifier.Queen, false);
        public static GamePiece BlackRook { get; } = new GamePiece(GamePieceIdentifier.Rook, false);
        public static GamePiece BlackKnight { get; } = new GamePiece(GamePieceIdentifier.Knight, false);
        public static GamePiece BlackBishop { get; } = new GamePiece(GamePieceIdentifier.Bishop, false);
        public static GamePiece BlackKing { get; } = new GamePiece(GamePieceIdentifier.King, false);
        public static GamePiece BlackPawn { get; } = new GamePiece(GamePieceIdentifier.Pawn, false);
    }

}

﻿namespace GameLogic
{
    public record Piece 
    {
        public string Identifier { get; }

        internal Piece(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }
    }

    public class Pieces
    {
        public static Piece Queen { get; } = new Piece("Q");
        public static Piece Rook { get; } = new Piece("R");
        public static Piece Knight { get; } = new Piece("N");
        public static Piece Bishop { get; } = new Piece("B");
        public static Piece Pawn { get; } = new Piece("P");
        public static Piece King { get; } = new Piece("N");
    }

    public class TradablePieces
    {
        public static Piece Queen { get; } = Pieces.Queen;
        public static Piece Rook { get; } = Pieces.Rook;
        public static Piece Knight { get; } = Pieces.Knight;
        public static Piece Bishop { get; } = Pieces.Bishop;
    }

    public class ColoredPieces
    {
        public static Piece WhiteQueen { get; } = new Piece("q");
        public static Piece WhiteRook { get; } = new Piece("r");
        public static Piece WhiteKnight { get; } = new Piece("n");
        public static Piece WhiteBishop { get; } = new Piece("b");
        public static Piece WhiteKing { get; } = new Piece("k");
        public static Piece WhitePawn { get; } = new Piece("p");
        public static Piece BlackQueen { get; } = new Piece("Q");
        public static Piece BlackRook { get; } = new Piece("R");
        public static Piece BlackKnight { get; } = new Piece("N");
        public static Piece BlackBishop { get; } = new Piece("B");
        public static Piece BlackKing { get; } = new Piece("K");
        public static Piece BlackPawn { get; } = new Piece("P");
    }

}

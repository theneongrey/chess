namespace ChessApiContract
{
    public static class Calls
    {
        private const string BaseCall = "game";

        public const string NewGame = BaseCall;
        public const string GameList = BaseCall;
        public const string GameById = BaseCall;
        public const string AllowedMoves = BaseCall + "/allowed-moves";
        public const string MovePiece = BaseCall + "/move";
    }
}
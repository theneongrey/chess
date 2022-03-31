namespace ChessApi.Services
{
    public class IllegalMoveException : Exception
    {
        public IllegalMoveException(string reason) : base(reason)
        {

        }
    }
}

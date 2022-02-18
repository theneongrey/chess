namespace ChessApiClient
{
    public interface IChessApiClient 
    {
        Task<bool> NewGame();
    }
}

namespace ChessApiContract.Response;

public interface IGameResponse
{
    public bool WasSuccessful { get; }
    public string Error { get; }
}

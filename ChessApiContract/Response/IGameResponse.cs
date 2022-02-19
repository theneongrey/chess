namespace ChessApiContract.Response;

public interface IGameResponse<T>
{
    public bool WasSuccessful { get; }
    public string? Error { get; }
    static abstract T RespondError(string error);
}

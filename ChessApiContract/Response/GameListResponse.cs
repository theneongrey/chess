namespace ChessApiContract.Response;

public record GameListResponse : IGameResponse
{
    public bool WasSuccessful { get; }
    public string Error { get; }
    public IEnumerable<string> Games { get; }

    private GameListResponse(bool wasSuccessful, string error, IEnumerable<string> games)
    {
        WasSuccessful = true;
        Error = string.Empty;
        Games = games;
    }

    public static GameListResponse RespondError(string error) => new GameListResponse(false, error, Array.Empty<string>());
    public static GameListResponse RespondSuccess(IEnumerable<string> games) => new GameListResponse(true, string.Empty, games);
}

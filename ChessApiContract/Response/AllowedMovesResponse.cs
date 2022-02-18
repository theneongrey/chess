namespace ChessApiContract.Response;

public record AllowedMovesResponse : IGameResponse
{
    public bool WasSuccessful { get; }
    public string Error { get; }
    public IEnumerable<string> Positions { get; }

    private AllowedMovesResponse(bool wasSuccessFull, string error, IEnumerable<string> positions)
    {
        WasSuccessful = wasSuccessFull;
        Error = error;
        Positions = positions;
    }

    public static AllowedMovesResponse RespondError(string error) => new AllowedMovesResponse(false, error, Array.Empty<string>());
    public static AllowedMovesResponse RespondSuccess(IEnumerable<string> positions) => new AllowedMovesResponse(true, string.Empty, positions);
}


namespace ChessApiContract.Response;

public record MovePieceResponse : IGameResponse
{
    public bool WasSuccessful { get; }
    public string Error { get; }

    private MovePieceResponse(bool wasSuccessful, string error = "")
    {
        WasSuccessful = wasSuccessful;
        Error = error;
    }

    public static MovePieceResponse RespondError(string error) => new MovePieceResponse(false, error);
    public static MovePieceResponse RespondSuccess() => new MovePieceResponse(true);
}

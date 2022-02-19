using System.Text.Json.Serialization;

namespace ChessApiContract.Response;

public record MovePieceResponse : IGameResponse<MovePieceResponse>
{
    public bool WasSuccessful { get; init; }
    public string? Error { get; init; }


    [JsonConstructor]
    public MovePieceResponse()
    {

    }

    private MovePieceResponse(bool wasSuccessful, string error = "")
    {
        WasSuccessful = wasSuccessful;
        Error = error;
    }

    public static MovePieceResponse RespondError(string error) => new MovePieceResponse(false, error);
    public static MovePieceResponse RespondSuccess() => new MovePieceResponse(true);
}

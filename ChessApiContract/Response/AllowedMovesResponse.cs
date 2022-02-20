using System.Text.Json.Serialization;

namespace ChessApiContract.Response;

public record AllowedMovesResponse : IGameResponse<AllowedMovesResponse>
{
    public bool WasSuccessful { get; init; }
    public string? Error { get; init; }
    public IEnumerable<string>? Positions { get; init; }


    [JsonConstructor]
    public AllowedMovesResponse()
    {

    }

    private AllowedMovesResponse(bool wasSuccessFull, string error, IEnumerable<string> positions)
    {
        WasSuccessful = wasSuccessFull;
        Error = error;
        Positions = positions;
    }

    public static AllowedMovesResponse RespondError(string error) => new AllowedMovesResponse(false, error, Array.Empty<string>());
    public static AllowedMovesResponse RespondSuccess(IEnumerable<string> positions) => new AllowedMovesResponse(true, string.Empty, positions);
}


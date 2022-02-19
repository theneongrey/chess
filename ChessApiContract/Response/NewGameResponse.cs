using System.Text.Json.Serialization;

namespace ChessApiContract.Response;

public record NewGameResponse : IGameResponse<NewGameResponse>
{
    public bool WasSuccessful { get; init; }
    public string? Error { get; init; }
    public Guid GameId { get; init; }

    [JsonConstructor]
    public NewGameResponse()
    {

    }

    private NewGameResponse(bool wasSuccessful, string error, Guid gameId)
    {
        WasSuccessful = wasSuccessful;
        Error = error;
        GameId = gameId;
    }

    public static NewGameResponse RespondError(string error) => new NewGameResponse(false, error, Guid.Empty);
    public static NewGameResponse RespondSuccess(Guid gameId) => new NewGameResponse(true, string.Empty, gameId);
}


using System.Text.Json.Serialization;

namespace ChessApiContract.Response;

public record GameListResponse : IGameResponse<GameListResponse>
{
    public bool WasSuccessful { get; init; }
    public string? Error { get; init; }
    public IEnumerable<Guid>? Games { get; init; }


    [JsonConstructor]
    public GameListResponse()
    {
        
    }

    private GameListResponse(bool wasSuccessful, string error, IEnumerable<Guid> games)
    {
        WasSuccessful = wasSuccessful;
        Error = error;
        Games = games;
    }

    public static GameListResponse RespondError(string error) => new GameListResponse(false, error, Array.Empty<Guid>());
    public static GameListResponse RespondSuccess(IEnumerable<Guid> games) => new GameListResponse(true, string.Empty, games);
}

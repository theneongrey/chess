using System.Text.Json.Serialization;

namespace ChessApiContract.Response;

public record GetGameResponse : IGameResponse<GetGameResponse>
{
    public bool WasSuccessful { get; init; }
    public string? Error { get; init; }
    public IEnumerable<string>? Cells { get; init; }
    public string? State { get; init; }
    public bool IsItWhitesTurn { get; init; }
    public bool IsCheckPending { get; init; }


    [JsonConstructor]
    public GetGameResponse()
    {

    }

    private GetGameResponse(bool wasSuccessful, string error, IEnumerable<string> cells, string state, bool isItWhitesTurn, bool isCheckPending)
    {
        WasSuccessful = wasSuccessful;
        Error = error;
        Cells = cells;
        State = state;
        IsItWhitesTurn = isItWhitesTurn;
        IsCheckPending = isCheckPending;
    }

    public static GetGameResponse RespondError(string error) => new GetGameResponse(false, error, Array.Empty<string>(), string.Empty, false, false);
    public static GetGameResponse RespondSuccess(IEnumerable<string> cells, string state, bool isItWhitesTurn, bool isCheckPending)
        => new GetGameResponse(true, string.Empty, cells, state, isItWhitesTurn, isCheckPending);
}


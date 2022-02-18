namespace ChessApiContract.Response;

public record GetGameResponse : IGameResponse
{
    public bool WasSuccessful { get; }
    public string Error { get; }
    public IEnumerable<string> Cells { get; }
    public string State { get; }
    public bool IsItWhitesTurn { get; }
    public bool IsCheckPending { get; }

    private GetGameResponse(IEnumerable<string> cells, string state, bool isItWhitesTurn, bool isCheckPending)
    {
        WasSuccessful = true;
        Error = string.Empty;
        Cells = cells;
        State = state;
        IsItWhitesTurn = isItWhitesTurn;
        IsCheckPending = isCheckPending;
    }

    private GetGameResponse(string error)
    {
        WasSuccessful = false;
        Error = error;
        Cells = Array.Empty<string>();
        State = string.Empty;
        IsItWhitesTurn = false;
        IsCheckPending = false;
    }

    public static GetGameResponse RespondError(string error) => new GetGameResponse(error);
    public static GetGameResponse RespondSuccess(IEnumerable<string> cells, string state, bool isItWhitesTurn, bool isCheckPending)
        => new GetGameResponse(cells, state, isItWhitesTurn, isCheckPending);
}


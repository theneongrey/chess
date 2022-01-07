namespace MinimalChessApi.Results;

public record NewGameResult (string GameId);
public record BoardResult (IEnumerable<string> Cells);


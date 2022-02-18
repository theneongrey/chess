namespace MinimalChessApi.Model;

public record GameReferenceModel(Guid GameId);
public record GameModel(IEnumerable<string> Cells, string State, bool IsItWhitesTurn, bool IsCheckPending);
public record AllowedMoves(IEnumerable<string> Positions);


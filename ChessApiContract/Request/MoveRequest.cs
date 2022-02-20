namespace ChessApiContract.Request;

public record MoveRequest(string FromCellName, string ToCellName);

namespace ChessApiContract.Response
{
    public record ErrorResponse (string Type, string Title, int Status, string Detail);
}

namespace ChessApi.Dto
{
    public record GameDto(Guid Id, string[] Cells, string State, bool IsItWhitesTurn, bool IsCheckPending, UserDto Player1, UserDto Player2);
}

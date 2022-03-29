namespace ChessApi.Dto
{
    public record GameShortDto(Guid Id, string State, UserDto Player1, UserDto Player2);
}

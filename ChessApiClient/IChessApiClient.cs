using ChessApiContract.Response;

namespace ChessApiClient
{
    public interface IChessApiClient 
    {
        Task<NewGameResponse> NewGameAsync();
        Task<GameListResponse> GetGameListAsync();
        Task<GetGameResponse> GetGameAsync(Guid gameId);
        Task<MovePieceResponse> MovePieceAsync(Guid gameId, string fromCellName, string toCellName);
        Task<AllowedMovesResponse> GetAllowedMovesAsync(Guid gameId, string pieceCellName);
    }
}

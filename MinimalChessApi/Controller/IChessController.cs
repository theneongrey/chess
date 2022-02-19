using ChessApiContract.Response;

namespace MinimalChessApi.Controller
{
    public interface IChessController
    {
        Task<NewGameResponse> NewGameAsync();
        Task<GameListResponse> GetGameListAsync();
        Task<GetGameResponse> GetGameAsync(Guid gameId);
        Task<MovePieceResponse> MovePieceAsync(Guid gameId, string fromCellName, string toCellName);
        Task<AllowedMovesResponse> GetAllowedMovesAsync(Guid id, string pieceCellName);
    }
}

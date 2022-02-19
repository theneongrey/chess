namespace MinimalChessApi.Services
{
    public interface IGameStoreService
    {
        Task<bool> SaveGameAsync(Guid gameId, string game);
        Task<string?> LoadGameAsync(Guid gameId);
        Task<List<Guid>?> GetGamesAsync();
    }
}

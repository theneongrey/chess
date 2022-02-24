using System.IO.Abstractions;

namespace MinimalChessApi.Services
{
    public class FileStoreService : IGameStoreService
    {
        private string _targetPath;
        private const string GameExtension = "game";
        private IFileSystem _fileSystem;

        public FileStoreService(IFileSystem fileSystem, string targetPath)
        {
            _targetPath = targetPath;
            _fileSystem = fileSystem;

            // it's only for testing, so don't spam everything with files.
            // create a clean dir every time
            SetupCleanTargetDir();
        }
        private Task SetupCleanTargetDir()
        {
            if (_fileSystem.Directory.Exists(_targetPath))
            {
                _fileSystem.Directory.Delete(_targetPath, true);
            }

            _fileSystem.Directory.CreateDirectory(_targetPath);

            return Task.CompletedTask;
        }

        private string GetGameFilename(Guid gameId)
        {
            return Path.Combine(_targetPath, $"{gameId}.{GameExtension}");
        }

        public Task<string?> LoadGameAsync(Guid gameId)
        {
            var filename = GetGameFilename(gameId);
            if (_fileSystem.File.Exists(filename))
            {
                try
                {
                    return _fileSystem.File.ReadAllTextAsync(filename)!;
                }
                catch { }
            }

            return Task.FromResult(null as string);
        }

        public async Task<bool> SaveGameAsync(Guid gameId, string game)
        {
            try
            {
                var filename = GetGameFilename(gameId);
                await _fileSystem.File.WriteAllTextAsync(filename, game);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<List<Guid>?> GetGamesAsync()
        {
            if (_fileSystem.Directory.Exists(_targetPath))
            {
                var result = new List<Guid>();
                foreach (var file in _fileSystem.Directory.GetFiles(_targetPath, $"*.{GameExtension}"))
                {
                    var guidCandidate = Path.GetFileNameWithoutExtension(file);
                    if (Guid.TryParse(guidCandidate, out var guid))
                    {
                        result.Add(guid);
                    }
                }

                return Task.FromResult(result)!;
            }

            return Task.FromResult(null as List<Guid>);
        }
    }
}

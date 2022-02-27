using FluentAssertions;
using MinimalChessApi.Services;
using NSubstitute;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MinimalChessApi.Test.Unit
{
    public class FileStoreServiceTest
    {
        private const string TargetPath = "_gamesTest";
        private IFileSystem _fileSystemMock;
        private FileStoreService _sut;

        public FileStoreServiceTest()
        {
            _fileSystemMock = Substitute.For<IFileSystem>();
            _sut = new FileStoreService(_fileSystemMock, TargetPath);
        }

        #region save game
        [Fact]
        public async Task SaveGame_ValidNewGame_ReturnsTrue()
        {
            // Arrange
            string? actualFileContent = null;
            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);

            await _fileSystemMock.File.WriteAllTextAsync(Arg.Any<string>(), Arg.Do<string>(c => actualFileContent = c));


            // Act
            var result = await _sut.SaveGameAsync(Guid.NewGuid(), string.Empty);

            // Assert
            result.Should().BeTrue();
            actualFileContent.Should().Be(string.Empty);
        }

        [Fact]
        public async Task SaveGame_ValidRunningGame_ReturnsTrue()
        {
            // Arrange
            string fileContent = @"1.f2-f4 e7-e6
2.Ng1-h3 Bf8-e7
3.a2-a4 Be7-h4+";
            string? actualFileContent = null;
            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);

            await _fileSystemMock.File.WriteAllTextAsync(Arg.Any<string>(), Arg.Do<string>(c => actualFileContent = c));

            // Act
            var result = await _sut.SaveGameAsync(Guid.NewGuid(), fileContent);

            // Assert
            result.Should().BeTrue();
            actualFileContent.Should().Be(fileContent);
        }

        [Fact]
        public async Task SaveGame_OnWriteException_ReturnsFalse()
        {
            // Arrange
            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.File.WriteAllTextAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(b => throw new Exception());

            // Act
            var result = await _sut.SaveGameAsync(Guid.NewGuid(), string.Empty);

            // Assert
            result.Should().BeFalse();
        }
        #endregion

        #region load game
        public async Task LoadGame_ExistingGame_ReturnsGame()
        {
            // Arrange
            string fileContent = @"1.f2-f4 e7-e6
2.Ng1-h3 Bf8-e7
3.a2-a4 Be7-h4+";
            var gameId = Guid.NewGuid();
            var filename = Path.Combine(TargetPath, $"{gameId}.game"); ;

            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.File.Exists(filename).Returns(true);
            _fileSystemMock.File.ReadAllTextAsync(filename).Returns(fileContent);

            // Act
            var result = await _sut.LoadGameAsync(new Guid());

            // Assert
            result.Should().Be(fileContent);
        }

        [Fact]
        public async Task LoadGame_NonExistingGame_ReturnsNull()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var filename = Path.Combine(TargetPath, $"{gameId}.game"); ;

            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.File.Exists(filename).Returns(false);

            // Act
            var result = await _sut.LoadGameAsync(new Guid());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoadGame_WhenException_ReturnsNull()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var filename = Path.Combine(TargetPath, $"{gameId}.game"); ;

            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.File.Exists(filename).Returns(true);
            _fileSystemMock.File.ReadAllTextAsync(filename).Returns(async x => throw new Exception());

            // Act
            var result = await _sut.LoadGameAsync(new Guid());

            // Assert
            result.Should().BeNull();
        }
        #endregion

        #region get 
        [Fact]
        public async Task GetGamesAsync_OpenGames_ReturnsGameList()
        {
            // Arrange
            var games = new[] {new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba"),
                               new Guid("37b317a0-ff83-42e6-8175-54cf6e1a35e0"),
                               new Guid("ad90b4bc-27cb-455f-8439-bc2318ae656a")};

            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.Directory.GetFiles(TargetPath, Arg.Any<string>())
                .Returns(games.Select(g => Path.Combine(TargetPath, g+".game")).ToArray());

            // Act
            var result = await _sut.GetGamesAsync();

            // Assert
            result.Should().BeEquivalentTo(games);
        }

        [Fact]
        public async Task GetGamesAsync_NoGame_ReturnsEmptyList()
        {
            // Arrange
            _fileSystemMock.Directory.Exists(TargetPath).Returns(true);
            _fileSystemMock.Directory.GetFiles(TargetPath, Arg.Any<string>())
                .Returns(Array.Empty<string>().ToArray());

            // Act
            var result = await _sut.GetGamesAsync();

            // Assert
            result.Should().BeEmpty();
        }
        #endregion
    }
}

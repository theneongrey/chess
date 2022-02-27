using ChessApiContract;
using ChessApiContract.Response;
using FluentAssertions;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using MinimalChessApi.Test.e2e.Mocks;
using MinimalChessApi.Services;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using ChessApiContract.Request;
using System.Text;

namespace MinimalChessApi.Test.Integration
{
    public class MinimalChessApiTestAppFixture
    {
        private MinimalChessApiTestApp _sut { get; }

        public HttpClient HttpClient { get; }

        public MinimalChessApiTestAppFixture()
        {
            _sut = new MinimalChessApiTestApp(services =>
            {
                services.AddSingleton<IFileSystem, MockFileSystem>();
                services.AddSingleton<IGameStoreService>(i =>
                   ActivatorUtilities.CreateInstance<FileStoreService>(i, "C:\\_gameTest")
                );
            });
            HttpClient = _sut.CreateClient();
        }
    }

    [TestCaseOrderer("MinimalChessApi.Test.Integration.Helper.AlphabeticalOrderer", "MinimalChessApi.Test")]
    public class MinimalChessApiTest : IClassFixture<MinimalChessApiTestAppFixture>
    {
        private readonly HttpClient _httpClient;

        public MinimalChessApiTest(MinimalChessApiTestAppFixture minimalChessApiTestAppFixture)
        {
            _httpClient = minimalChessApiTestAppFixture.HttpClient;
        }

        #region Helper
        private async Task<T> ResponseToInstanceAsync<T>(HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })!;
        }

        private async Task<T> PerformCallAsync<T>(HttpRequestMessage message)
        {
            var response = await _httpClient.SendAsync(message);
            response.IsSuccessStatusCode.Should().BeTrue();

            return await ResponseToInstanceAsync<T>(response);
        }

        private async Task<Guid> GetFirstGameAsync()
        {
            var gameList = await PerformCallAsync<GameListResponse>(new HttpRequestMessage(HttpMethod.Get, Calls.GameList));
            gameList.WasSuccessful.Should().BeTrue();
            gameList.Games.Should().NotBeEmpty();

            return gameList.Games!.First();
        }
        #endregion

        [Fact] 
        public async Task Test1000_GameList_ShouldBeEmpty()
        {
            var gameList = await PerformCallAsync<GameListResponse>(new HttpRequestMessage(HttpMethod.Get, Calls.GameList));
            gameList.WasSuccessful.Should().BeTrue();
            gameList.Games.Should().BeEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Test1100_CreateGames_ShouldSucceed(int _)
        {
            var game = await PerformCallAsync<NewGameResponse>(new HttpRequestMessage(HttpMethod.Post, Calls.NewGame));
            game.WasSuccessful.Should().BeTrue();
        }

        [Fact]
        public async Task Test1200_GameList_ShouldHave_4_Entries()
        {
            var gameList = await PerformCallAsync<GameListResponse>(new HttpRequestMessage(HttpMethod.Get, Calls.GameList));
            gameList.WasSuccessful.Should().BeTrue();
            gameList.Games.Should().HaveCount(4);
        }

        [Fact]
        public async Task Test1400_GetGameByValidId_ShouldReturnGame()
        {
            var firstGame = await GetFirstGameAsync();

            var game = await PerformCallAsync<GetGameResponse>(new HttpRequestMessage(HttpMethod.Get, Calls.GameById + $"/{firstGame}"));
            game.WasSuccessful.Should().BeTrue();
            game.Error.Should().BeEmpty();
            game.State.Should().Be("Running");
            game.IsItWhitesTurn.Should().BeTrue();
            game.Cells.Should().HaveCount(64);
        }


        [Fact]
        public async Task Test1310_GetGameByInvalidId_ShouldReturnError()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Calls.GameById + $"/{Guid.NewGuid()}"));
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Test1400_GetAllowedMovesWithValidParameters_ShoudReturnValidPositions()
        {
            var firstGame = await GetFirstGameAsync();

            var moves = await PerformCallAsync<AllowedMovesResponse>(new HttpRequestMessage(HttpMethod.Get, Calls.AllowedMoves + $"/{firstGame}/A2"));
            moves.WasSuccessful.Should().BeTrue();
            moves.Positions.Should().HaveCount(2);
            moves.Positions.Should().Contain("A3", "A4");
        }

        [Fact]
        public async Task Test1410_GetAllowedMovesWithInvalidGameId_ShoudReturnError()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Calls.AllowedMoves + $"/{Guid.NewGuid()}/A2"));
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Test1420_GetAllowedMovesWithInvalidCell_ShoudReturnError()
        {
            var firstGame = await GetFirstGameAsync();

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Calls.AllowedMoves + $"/{firstGame}/X2"));
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Test1500_MovePieceWithValidData_ShouldReturnSuccess()
        {
            var firstGame = await GetFirstGameAsync();

            var serializedRequestObject = JsonSerializer.Serialize(new MoveRequest("A2", "A4"));
            var message = new HttpRequestMessage(HttpMethod.Put, Calls.MovePiece + $"/{firstGame}");
            message.Content = new StringContent(serializedRequestObject, Encoding.UTF8, "application/json");

            var result = await PerformCallAsync<MovePieceResponse>(message);
            result.WasSuccessful.Should().BeTrue();
        }

        [Fact]
        public async Task Test1500_MovePieceWithNoBody_ShouldReturnError()
        {
            var firstGame = await GetFirstGameAsync();

            var message = new HttpRequestMessage(HttpMethod.Put, Calls.MovePiece + $"/{firstGame}");

            var response = await _httpClient.SendAsync(message);
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Test1500_MovePieceWithNoNonExistingGameId_ShouldReturnError()
        {
            var serializedRequestObject = JsonSerializer.Serialize(new MoveRequest("A2", "A4"));
            var message = new HttpRequestMessage(HttpMethod.Put, Calls.MovePiece + $"/{Guid.NewGuid()}");
            message.Content = new StringContent(serializedRequestObject, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        // TODO: Test get on post calls
        // TODO: More error tests
    }
}

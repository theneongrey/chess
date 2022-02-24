using FluentAssertions;
using MinimalChessApi.Controller;
using MinimalChessApi.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MinimalChessApi.Test.Unit
{
    public class ChessControllerTest
    {
        #region new game
        [Fact]
        public async Task NewGame_WithValidStoreService_ReturnsValidGame()
        {
            // Arrange
            var storeServiceMock = Substitute.For<IGameStoreService>();
            Guid gameGuid = default;
            storeServiceMock.SaveGameAsync(Arg.Do<Guid>(guid => gameGuid = guid), Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.NewGameAsync();

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.GameId.Should().Be(gameGuid);
        }

        [Fact]
        public async Task NewGame_WithInvalidStoreService_ReturnsErrorMessage()
        {
            // Arrange
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.SaveGameAsync(Arg.Any<Guid>(), Arg.Any<string>()).Returns(false);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.NewGameAsync();

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().NotBeEmpty();
            newGameResult.GameId.Should().Be(Guid.Empty);
        }
        #endregion

        #region get game list
        [Fact]
        public async Task GetGameList_WithNoGames_ReturnsEmptyGameList()
        {
            // Arrange
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.GetGamesAsync().Returns(new List<Guid>());

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameListAsync();

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Games.Should().BeEmpty();
        }

        [Fact]
        public async Task GetGameList_WithGames_ReturnsGameList()
        {
            // Arrange
            var games = new List<Guid> {
                            new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba"),
                            new Guid("37b317a0-ff83-42e6-8175-54cf6e1a35e0"),
                            new Guid("ad90b4bc-27cb-455f-8439-bc2318ae656a")
                        };

            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.GetGamesAsync().Returns(games);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameListAsync();

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Games.Should().HaveCount(3);
            newGameResult.Games.Should().BeEquivalentTo(games);
        }


        [Fact]
        public async Task GetGameList_WithBrokenStoreService_ReturnsError()
        {
            // Arrange
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.GetGamesAsync().Returns(null as List<Guid>);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameListAsync();

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().NotBeEmpty();
            newGameResult.Games.Should().HaveCount(0);
        }
        #endregion

        #region get game

        [Fact]
        public async Task GetGame_WithNewGame_ReturnsNewGame()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var cells = new string[] {
            "r", "n", "b", "q", "k", "b", "n", "r",
            "p", "p", "p", "p", "p", "p", "p", "p",
            "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "",
            "P", "P", "P", "P", "P", "P", "P", "P",
            "R", "N", "B", "Q", "K", "B", "N", "R" };

            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameAsync(gameId);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Cells.Should().BeEquivalentTo(cells);
            newGameResult.State.Should().Be("Running");
            newGameResult.IsCheckPending.Should().BeFalse();
            newGameResult.IsItWhitesTurn.Should().BeTrue();
        }

        [Fact]
        public async Task GetGame_WithOneMove_ReturnsGameWithNonWhiteMove()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var cells = new string[] {
            "r", "n", "b", "q", "k", "b", "n", "r",
            "p", "", "p", "p", "p", "p", "p", "p",
            "", "", "", "", "", "", "", "",
            "", "p", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "",
            "P", "P", "P", "P", "P", "P", "P", "P",
            "R", "N", "B", "Q", "K", "B", "N", "R" };

            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns("1.a2-a4");

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameAsync(gameId);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Cells.Should().BeEquivalentTo(cells);
            newGameResult.State.Should().Be("Running");
            newGameResult.IsCheckPending.Should().BeFalse();
            newGameResult.IsItWhitesTurn.Should().BeFalse();
        }

        [Fact]
        public async Task GetGame_WithCheckPending_ReturnsGameWithCheckPending()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var cells = new string[] {
            "r", "n", "b", "q", "k", "b", "", "r",
            "", "p", "p", "p", "p", "", "p", "p",
            "", "", "", "", "", "", "", "n",
            "p", "", "", "", "", "p", "", "B",
            "", "", "", "", "", "", "", "",
            "", "", "", "", "P", "", "", "",
            "P", "P", "P", "P", "", "P", "P", "P",
            "R", "N", "B", "Q", "K", "", "N", "R" };

            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(@"1.f2-f4 e7-e6
2.Ng1-h3 Bf8-e7
3.a2-a4 Be7-h4+");

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameAsync(gameId);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Cells.Should().BeEquivalentTo(cells);
            newGameResult.State.Should().Be("Running");
            newGameResult.IsCheckPending.Should().BeTrue();
            newGameResult.IsItWhitesTurn.Should().BeTrue();
        }

        [Fact]
        public async Task GetGame_WithBrokenStoreService_ReturnsError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(null as string);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetGameAsync(gameId);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().NotBeEmpty();
            newGameResult.Cells.Should().BeEmpty();
            newGameResult.State.Should().BeEmpty();
            newGameResult.IsCheckPending.Should().BeFalse();
            newGameResult.IsItWhitesTurn.Should().BeFalse();
        }
        // TODO: tests for different states
        #endregion

        #region move piece

        [Fact]
        public async Task MovePiece_WithValidInput_ShouldBeSuccessful()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, "A2", "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
        }

        [Fact]
        public async Task MovePiece_WithUnknownGame_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(null as string);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, "A2", "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"Game \"{gameId}\" could not be loaded");
        }

        [Fact]
        public async Task MovePiece_WithInvalidFromCell_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);
            var from = "X0";

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, from, "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"\"From\" position \"{from}\" could not be interpreted");
        }

        [Fact]
        public async Task MovePiece_WithInvalidToCell_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);
            var to = "X0";

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, "A2", to);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"\"To\" position \"{to}\" could not be interpreted");
        }

        [Fact]
        public async Task MovePiece_WithFromCellWithNoPiece_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);
            var from = "A3";

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, from, "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"There is no valid piece at \"from\" position \"{from}\"");
        }

        [Fact]
        public async Task MovePiece_WithFromCellWithBlackPieceOnWhiteTurn_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(true);

            var sut = new ChessController(storeServiceMock);
            var from = "A7";

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, from, "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"There is no valid piece at \"from\" position \"{from}\"");
        }

        [Fact]
        public async Task MovePiece_WithErrorsWhileWriting_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            storeServiceMock.SaveGameAsync(gameId, Arg.Any<string>()).Returns(false);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.MovePieceAsync(gameId, "A2", "A4");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"Could not update game");
        }
        #endregion

        #region get allowed moves
        [Fact]
        public async Task GetAllowedMoves_ForValidPiece_ShouldReturnListOfAllowedPositions()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);
            
            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetAllowedMovesAsync(gameId, "A2");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeTrue();
            newGameResult.Error.Should().BeEmpty();
            newGameResult.Positions.Should().BeEquivalentTo(new[]{ "A3", "A4" });
        }

        [Fact]
        public async Task GetAllowedMoves_ForNotExistingGame_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(null as string);

            var sut = new ChessController(storeServiceMock);

            // Act
            var newGameResult = await sut.GetAllowedMovesAsync(gameId, "A2");

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"Game \"{gameId}\" could not be loaded");
        }

        [Fact]
        public async Task GetAllowedMoves_ForEmptyCell_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);

            var sut = new ChessController(storeServiceMock);
            var cell = "A3";

            // Act
            var newGameResult = await sut.GetAllowedMovesAsync(gameId, cell);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"There is no valid piece at given position \"{cell}\"");
        }

        [Fact]
        public async Task GetAllowedMoves_ForInvalidCell_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);

            var sut = new ChessController(storeServiceMock);
            var cell = "X2";

            // Act
            var newGameResult = await sut.GetAllowedMovesAsync(gameId, cell);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"Given position \"{cell}\" could not be interpreted");
        }

        [Fact]
        public async Task GetAllowedMoves_ForCellNotInTurn_ShouldReturnError()
        {
            // Arrange
            var gameId = new Guid("3bc2693e-7d86-443e-a807-e6baea67bfba");
            var storeServiceMock = Substitute.For<IGameStoreService>();
            storeServiceMock.LoadGameAsync(gameId).Returns(string.Empty);

            var sut = new ChessController(storeServiceMock);
            var cell = "A7";

            // Act
            var newGameResult = await sut.GetAllowedMovesAsync(gameId, cell);

            // Assert
            newGameResult.Should().NotBeNull();
            newGameResult.WasSuccessful.Should().BeFalse();
            newGameResult.Error.Should().Be($"There is no valid piece at given position \"{cell}\"");
        }
        #endregion

    }
}
using ChessApiClient;
using Cocona;

namespace ChessApiConsoleClient.Commands
{
    public static class CoconaCommandMapper
    {
        private static async Task NewGame(IChessApiClient client)
        {
            var result = await client.NewGameAsync();
            if (result.WasSuccessful)
            {
                Console.WriteLine($"New game created with id {result.GameId}");
            }
            else
            {
                Console.Error.WriteLine(result.Error);
            }
        }

        private static async Task ListGames(IChessApiClient client)
        {
            var result = await client.GetGameListAsync();
            if (result.WasSuccessful)
            {
                Console.WriteLine("Available games:");
                foreach (var game in result.Games)
                {
                    Console.WriteLine(game);
                }
            }
            else
            {
                Console.Error.WriteLine(result.Error);
            }
        }

        private static async Task ShowGameById(IChessApiClient client, string game)
        {
            Guid gameId;
            if (!Guid.TryParse(game, out gameId))
            {
                Console.Error.WriteLine("Game id is no valid guid");
                return;
            }

            var result = await client.GetGameAsync(gameId);
            if (result.WasSuccessful)
            {
                if (result.Cells?.Count() != 64)
                {
                    Console.WriteLine("Board is formatted incorrectly");
                    return;
                }

                Console.WriteLine($"Game {game}:");
                Console.WriteLine($"State: {result.State}");
                Console.WriteLine($"Current turn: {(result.IsItWhitesTurn ? "White" : "Black")}");
                if (result.IsCheckPending)
                {
                    Console.WriteLine("!CHECK PENDING!");
                }
                Console.WriteLine();

                var cellArray = result.Cells.ToArray();
                for (var y = 7; y >= 0; y--)
                {
                    Console.Write(y+1+"|");
                    for (var x = 0; x < 8; x++)
                    {
                        var cellIndex = (y * 8) + x;
                        var cell = cellArray[cellIndex];
                        if (string.IsNullOrEmpty(cell))
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write(cell);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(" ---------");
                Console.WriteLine(" |12345678");
            }
            else
            {
                Console.Error.WriteLine(result.Error);
            }
        }

        private static async Task ShowAllowedMoves(IChessApiClient client, string game, string cell)
        {
            Guid gameId;
            if (!Guid.TryParse(game, out gameId))
            {
                Console.Error.WriteLine("Game id is no valid guid");
                return;
            }

            var result = await client.GetAllowedMovesAsync(gameId, cell);
            if (result.WasSuccessful)
            {
                Console.WriteLine($"Available moves for {cell}:");
                foreach (var position in result.Positions)
                {
                    Console.WriteLine(position);
                }
            }
            else
            {
                Console.Error.WriteLine(result.Error);
            }
        }

        private static async Task MovePiece(IChessApiClient client, string game, string from, string to)
        {
            Guid gameId;
            if (!Guid.TryParse(game, out gameId))
            {
                Console.Error.WriteLine("Game id is no valid guid");
                return;
            }

            var result = await client.MovePieceAsync(gameId, from, to);
            if (result.WasSuccessful)
            {
                Console.WriteLine($"Piece moved from {from} to {to}");
            }
            else
            {
                Console.Error.WriteLine(result.Error);
            }
        }

        public static void MapEndPoints(this CoconaApp app)
        {
            app.AddCommand("new", NewGame);
            app.AddCommand("games", ListGames);
            app.AddCommand("show", ShowGameById);
            app.AddCommand("allowed", ShowAllowedMoves);
            app.AddCommand("move", MovePiece);
        }
    }
}

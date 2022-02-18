using ChessApiClient;
using Cocona;

namespace ChessApiConsoleClient.Commands
{
    public static class CoconaCommandMapper
    {
        private static async Task NewGame(IChessApiClient client)
        {
            if (await client.NewGame())
            {
                Console.WriteLine("");
            }
        }

        private static Task ListGames(IChessApiClient client)
        {
            return Task.CompletedTask;
        }

        private static Task ShowGameById(IChessApiClient client, string game)
        {
            return Task.CompletedTask;
        }

        private static Task ShowAllowedMoves(IChessApiClient client, string game, string cell)
        {
            return Task.CompletedTask;
        }

        private static Task MovePiece(IChessApiClient client, string game, string from, string to)
        {
            return Task.CompletedTask;
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

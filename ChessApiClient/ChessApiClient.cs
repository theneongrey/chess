namespace ChessApiClient
{
    public class ChessApiClient : IChessApiClient
    {
        private HttpClient _httpClient;

        public ChessApiClient(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<bool> NewGame()
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "game"));
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
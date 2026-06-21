using Parcial2.Model;
using System.Net;
using System.Net.Http.Json;

namespace Parcial2.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        
        private const string GET_URL = "https://www.freetogame.com/api/games";
        private const string POST_URL = "https://jsonplaceholder.typicode.com/posts";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }



        public async Task<List<Game>> GetGames()
        {
            var response = await _httpClient.GetAsync(GET_URL);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var apiGames =
                    await response.Content.ReadFromJsonAsync<List<ApiGame>>();

                if (apiGames == null)
                    return new List<Game>();

                return apiGames.Select(g => new Game
                {
                    Name = g.title,
                    Genre = g.genre,
                    Developer = g.developer,
                    Publisher = g.publisher
                }).ToList();
            }

            throw new Exception(
                $"Error HTTP: {response.StatusCode}");
           
        }

        public async Task AddGame(Game game)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    POST_URL,
                    game);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Error al guardar: {response.StatusCode}");
            }
        }
    }
}
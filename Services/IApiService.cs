using Parcial2.Model;

namespace Parcial2.Services
{
    public interface IApiService
    {
        Task<List<Game>> GetGames();
        Task AddGame(Game game);
    }
}

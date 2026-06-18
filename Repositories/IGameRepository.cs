using Parcial2.Model;

namespace Parcial2.Repositories
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllAsync();
        Task<int> SaveAsync(Game game);
        Task<int> UpdateAsync(Game game);
        Task<int> DeleteAsync(Game game);
        Task<List<Game>> SearchAsync(string text);
    }
}
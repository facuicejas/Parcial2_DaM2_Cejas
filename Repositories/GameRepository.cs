using Parcial2.DataBase;
using Parcial2.Model;

namespace Parcial2.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameDataBase _database;

        public GameRepository(GameDataBase database)
        {
            _database = database;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _database.GetGamesAsync();
        }

        public async Task<int> SaveAsync(Game game)
        {
            return await _database.SaveGameAsync(game);
        }

        public async Task<int> UpdateAsync(Game game)
        {
            return await _database.UpdateGameAsync(game);
        }

        public async Task<int> DeleteAsync(Game game)
        {
            return await _database.DeleteGameAsync(game);
        }

        public async Task<List<Game>> SearchAsync(string text)
        {
            return await _database.SearchGamesAsync(text);
        }
    }
}
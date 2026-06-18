using System;
using System.Collections.Generic;
using System.Text;
using Parcial2.Model;
using SQLite;

//Agregado para el Segundo parcial,
// Esto sirve para que la base de datos
// sea implementada y funcione correctamente en el programa

namespace Parcial2.DataBase
{
    public class GameDataBase
    {
        SQLiteAsyncConnection _database;

        public GameDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Game>().Wait(); // Agregado para el segundo Parcial, esto crea la tabla Game si no existe cuando se abre la aplicacion
        }

        public Task<List<Game>> GetGamesAsync() 
        {
            return _database.Table<Game>().ToListAsync();
        }

        public Task<int> SaveGameAsync(Game game)
        {
            if (game.Id != 0)
                return _database.UpdateAsync(game);

            return _database.InsertAsync(game);
        }

       public Task<int> UpdateGameAsync(Game game)
        {
        return _database.UpdateAsync(game);
        }
        public Task<int> DeleteGameAsync(Game game)
        {
            return _database.DeleteAsync(game);
        }

        public Task<List<Game>> SearchGamesAsync(string text) //Esto sirve para poder buscar el nombre directamente en SQLite
        {
            return _database.Table<Game>()
                .Where(g => g.Name.Contains(text))
                .ToListAsync();
        }

    }
}

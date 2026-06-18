using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Parcial2.DataBase;
using Parcial2.Model;


namespace Parcial2.ViewModels
{
    // Recibe parametros desde Shell Navigation
    [QueryProperty(nameof(Game), "Game")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly GameDataBase _database;

        public DetailViewModel(GameDataBase database)
        {
            _database = database;
        }

        [ObservableProperty]
        private Game? game;

        
        //Esto fue agregado para el segundo Parcial esta funcion va a actualizar un juego
        [RelayCommand]
        public async Task UpdateGame()
        {
            if (Game == null)
                return;

            await _database.UpdateGameAsync(Game);

            await Shell.Current.CurrentPage.DisplayAlertAsync(
                "Todo correcto",
                "El Juego ha sido actualizado",
                "Ok");

            await Shell.Current.GoToAsync(".."); //Esto refresca la lista de registros automaticamente
        }

    }
}

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Parcial2.DataBase;
using Parcial2.Messages;
using Parcial2.Model;
using Parcial2.Services;

namespace Parcial2.ViewModels
{
    public partial class AddGameViewModel : ObservableObject
    {
        private readonly ApiService _api;
        private readonly GameDataBase _database; //Agregado para Parcial 2,
                                                 //esto implementa la clase de la base de datos al programa

        public AddGameViewModel(ApiService api,GameDataBase database) //Agregado para Parcial 2,
                                                                      //Aca se le inyecta la nueva base de datos al programa
        {
            _api = api;
            _database = database;
        }

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? genre;

        [ObservableProperty]
        private string? developer;

        [ObservableProperty]
        private string? publisher;

        [ObservableProperty]
        private string? status;


        [RelayCommand]
        public async Task AddGame()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Status = "Debe ingresar un nombre";

                await Toast.Make("Falta el nombre del juego")
                    .Show();

                return;
            }

            try
            {
                var game = new Game
                {
                    Name = Name,
                    Genre = Genre,
                    Developer = Developer,
                    Publisher = Publisher
                };

                await _api.AddGame(game);
                await _database.SaveGameAsync(game); //Agregado para el segundo Parcial,
                                                     //esto va a insertar un nuevo registro dentro de la tabla Game

                WeakReferenceMessenger.Default.Send(
                    new GameAddedMessage
                    {
                        Game = game
                    });

                Status = "Juego agregado correctamente";

                await Toast.Make(
                    $"Juego {game.Name} agregado")
                    .Show();
            }
            catch (Exception ex)
            {
                Status = ex.Message;

                await Toast.Make("Error al guardar")
                    .Show();
            }
        }
    }
}
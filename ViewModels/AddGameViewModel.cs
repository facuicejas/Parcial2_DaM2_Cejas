using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Parcial2.Messages;
using Parcial2.Model;
using Parcial2.Services;

namespace Parcial2.ViewModels
{
    public partial class AddGameViewModel : ObservableObject
    {
        private readonly ApiService _api;

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

        public AddGameViewModel(ApiService api)
        {
            _api = api;
        }

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
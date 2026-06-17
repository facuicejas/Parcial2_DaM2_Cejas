using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Parcial2.Services;
using Parcial2.Views;
using Parcial2.Messages;
using Parcial2.Model;
using Parcial2.DataBase;
namespace Parcial2.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ApiService _api;
        private readonly GameDataBase _database; //Agregado para el segundo parcial,
                                                 //esto va a cargar los datos a SQLite


        [ObservableProperty]
        private ObservableCollection<Game> games = new();

        private List<Game> allGames = new();

        [ObservableProperty]
        private string status = "";

        [ObservableProperty]
        private string searchText = "";

        public MainViewModel(ApiService api, GameDataBase database)
        {
            _api = api;
            _database = database; //Agregado para el Parcial 2, se implementa la base de datos

            WeakReferenceMessenger.Default.Register<GameAddedMessage>(
                this,
                (r, m) =>
                {
                    Games.Insert(0, m.Game);
                    allGames.Insert(0, m.Game);
                });

            LoadGamesCommand.Execute(null);
        }

        [RelayCommand]
        public async Task LoadGames()
        {
            try
            {
                Status = "Cargando juegos...";

                var list = await _database.GetGamesAsync(); //Agregado para el Parcial 2,
                                                            //esto carga los datos de la base de datos SQLite

                allGames = list;

                Games.Clear();

                foreach (var game in list)
                {
                    Games.Add(game);
                }

                Status = $"Se cargaron {Games.Count} juego/s";

                await Toast.Make("Lista cargada").Show();
            }
            catch (Exception ex)
            {
                Status = ex.Message;

                await Toast.Make("Error al cargar").Show();
            }
        }

        [RelayCommand]
        public async Task FilterGames() //Esta funcion fue modificada para poder buscar los juegos de una forma mas directa de SQLite
        {
            try
            {
                List<Game> filtered;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    filtered = await _database.GetGamesAsync();
                }
                else
                {
                    filtered = await _database
                        .SearchGamesAsync(SearchText);
                }

                Games.Clear();

                foreach (var game in filtered)
                {
                    Games.Add(game);
                }
                Status = $"{Games.Count} resultado/s";
                }
                catch (Exception ex)
                 {
                Status = ex.Message;
                await Toast.Make("Hubo un error en la busqueda")
                    .Show();
                 }
        }

        [RelayCommand]
        private async Task GoToAdd()
        {
            await Shell.Current.GoToAsync(nameof(AddGamePage));
        }

        [RelayCommand]
        private async Task GoToDetail(Game game)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Game", game }
            };
            await Shell.Current.GoToAsync(
                nameof(DetailPage),
                parameters);
        }
    }
}
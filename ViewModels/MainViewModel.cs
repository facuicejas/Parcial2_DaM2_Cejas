using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Parcial2.Messages;
using Parcial2.Model;
using Parcial2.Services;
using System.Collections.ObjectModel;

namespace Parcial2.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ApiService _api;

        [ObservableProperty]
        private ObservableCollection<Game> games = new();

        private List<Game> allGames = new();

        [ObservableProperty]
        private string status = "";

        [ObservableProperty]
        private string searchText = "";

        public MainViewModel(ApiService api)
        {
            _api = api;

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

                var list = await _api.GetGames();

                allGames = list;

                Games.Clear();

                foreach (var game in list)
                {
                    Games.Add(game);
                }

                Status = $"Se cargaron {Games.Count} juegos";

                await Toast.Make("Lista cargada").Show();
            }
            catch (Exception ex)
            {
                Status = ex.Message;

                await Toast.Make("Error al cargar").Show();
            }
        }

        [RelayCommand]
        public void FilterGames()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Games.Clear();

                foreach (var g in allGames)
                    Games.Add(g);

                return;
            }

            var filtered = allGames
                .Where(g =>
                    g.Name != null &&
                    g.Name.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

            Games.Clear();

            foreach (var g in filtered)
            {
                Games.Add(g);
            }
        }
    }
}
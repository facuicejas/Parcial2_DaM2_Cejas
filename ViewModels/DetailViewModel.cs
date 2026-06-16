using CommunityToolkit.Mvvm.ComponentModel;
using Parcial2.Model;


namespace Parcial2.ViewModels
{
    // Recibe parámetros desde Shell Navigation
    [QueryProperty(nameof(Game), "Game")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Game? game;
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Parcial2.DataBase;
using Parcial2.Model;
using Parcial2.Repositories;


namespace Parcial2.ViewModels
{
    // Recibe parametros desde Shell Navigation
    [QueryProperty(nameof(Game), "Game")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly IGameRepository _repository;

        public DetailViewModel(IGameRepository repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        private Game? game;

        
        //Esto fue agregado para el segundo Parcial esta funcion va a actualizar un juego
        [RelayCommand]
        public async Task UpdateGame()
        {
            if (Game == null)
                return;

            await _repository.UpdateAsync(Game);

            await Shell.Current.CurrentPage.DisplayAlertAsync(
                "Todo correcto",
                "El Juego ha sido actualizado",
                "Ok");

            await Shell.Current.GoToAsync(".."); //Esto refresca la lista de registros automaticamente
        }

    }
}

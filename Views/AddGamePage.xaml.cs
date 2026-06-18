using Parcial2.ViewModels;


namespace Parcial2.Views;

public partial class AddGamePage : ContentPage
{
    public AddGamePage(AddGameViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

using Parcial2.Views;

namespace Parcial2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registro de rutas
            Routing.RegisterRoute(
                nameof(DetailPage),
                typeof(DetailPage));

            Routing.RegisterRoute(
                nameof(AddGamePage),
                typeof(AddGamePage));
        }
    }
}
namespace Parcial2.Model
{
    //Desde este documento se crea el modelo en la API para poder 
    // mostrar en la pantalla los datos del juego cuando se usa el boton
    // "Cargar juego"
    public class ApiGame
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? genre { get; set; }
        public string? developer { get; set; }
        public string? publisher { get; set; }
    }
}
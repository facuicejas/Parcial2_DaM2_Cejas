using SQLite; //Agregado para Parcial 2, esto implementa SQLite al codigo
using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial2.Model
{
    public class Game
    {
        [PrimaryKey] [AutoIncrement] //Agregado para Parcial 2,
                        //se le agrega una Primary Key para poder usar la base de datos correctamente
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
    }
}

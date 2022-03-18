using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Xamarin.Forms;

namespace PokePhone.Model
{
        public class MyPokemon
        {
            public string Name { get; set; }
            public string Image { get; set; }
            public string ImageFemelle { get; set; }
            public string ImageShiny { get; set; }
            public int Hp { get; set; }
            public int Attaque { get; set; }
            public int Defense { get; set; }
            public string Type { get; set; }
            public string Type2 { get; set; }
            public string Ability { get; set; }
            public string Gender { get; set; }
            //Id du pokémon dans l'Api (et la BDD)
            [PrimaryKey]
            public int Id { get; set; }
            public string CouleurType { get; set; }

            public string VersChaine()
            {
                return "Nom : " + this.Name + " Type " + this.Type + " Type2 " + this.Type2 + " Attaque " + this.Attaque.ToString() +
                " HP " + this.Hp.ToString() + " Defense " + this.Defense.ToString() + " Chemin de l'image " + this.Image.ToString();
            }
        }
}
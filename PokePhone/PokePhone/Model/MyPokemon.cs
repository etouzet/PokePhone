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
            public string Ability { get; set; }
            public string Gender { get; set; }
            //Id du pokémon dans l'Api (et la BDD), pour pouvoir l'identifier et afficher le bon dans la liste
            [PrimaryKey,AutoIncrement]
            public int Id { get; set; }
            //public Color couleurType { get; set; }

            public string VersChaine()
            {
                return "Nom : " + this.Name + " Type " + this.Type + " Attaque " + this.Attaque.ToString() +
                " HP " + this.Hp.ToString() + " Defense " + this.Defense.ToString() + " Chemin de l'image " + this.Image.ToString();
            }
        }
}
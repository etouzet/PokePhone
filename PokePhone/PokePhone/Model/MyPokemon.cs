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
            public string name { get; set; }
            public string image { get; set; }
            public int hp { get; set; }
            public int attaque { get; set; }
            public int defense { get; set; }
            public string type { get; set; }
            //Id du pokémon dans l'Api (et la BDD), pour pouvoir l'identifier et afficher le bon dans la liste
            [PrimaryKey,AutoIncrement]
            public int id { get; set; }
            //Cet attribut sert pour faire la couleur de fond lors de l'affichage du pokémon
            public Color couleurType { get; set; }

            public string VersChaine()
            {
                return "Nom : " + this.name + " Type " + this.type + " Attaque " + this.attaque.ToString() +
                " HP " + this.hp.ToString() + " Defense " + this.defense.ToString() + " Chemin de l'image " + this.image;
            }
        }
}
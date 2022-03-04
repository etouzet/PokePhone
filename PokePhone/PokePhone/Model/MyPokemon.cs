using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            //Id du pokémon dans l'Api, pour pouvoir l'identifier et afficher le bon dans la liste
            public int id { get; set; }
        }
}
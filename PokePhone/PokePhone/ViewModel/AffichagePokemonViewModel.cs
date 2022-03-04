using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PokePhone.Model;
using PokeApiNet;
using System.Threading.Tasks;

namespace PokePhone.ViewModel
{
    class AffichagePokemonViewModel : ContentView
    {
        public MyPokemon PokemonAfficher;
        public AffichagePokemonViewModel()
        {
            PokemonAfficher = new MyPokemon();
        }

        public void DefinirPokemonAfficher()
        {
            PokemonAfficher = null; /*Chopper le dernierPokemonTapper*/
        }
    }
}

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
        public AffichagePokemonViewModel()
        {
            MyPokemon PokemonAfficher = new MyPokemon();
            InitPokemon(PokemonAfficher);
        }

        public async void InitPokemon(MyPokemon pokemonAfficher)
        {
            
        }
    }
}

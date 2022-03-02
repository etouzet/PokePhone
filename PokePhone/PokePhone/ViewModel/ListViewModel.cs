using PokeApiNet;
using PokePhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokePhone.ViewModel
{
    public class ListViewModel : ContentView
    {
        public ListViewModel()
        {
            ListOfPokemon = new ObservableCollection<MyPokemon>();
            InitList();
        }

        public ObservableCollection<MyPokemon> ListOfPokemon { get; private set; }

        public async void InitList()
        { 
            PokeApiClient pokeClient = new PokeApiClient();

            for (int i = 1; i <= 20; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));
                MyPokemon mypokemon = new MyPokemon();
                mypokemon.name = pokemon.Name;
                mypokemon.image = pokemon.Sprites.FrontDefault;
                mypokemon.hp = pokemon.Stats[0].BaseStat;
                ListOfPokemon.Add(mypokemon);
            }
        }

    }
}

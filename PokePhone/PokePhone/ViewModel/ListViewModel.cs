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

            //Ajout des pokémons de l'API dans une liste utiliser par l'application pour afficher les pokémons
            for (int i = 1; i <= 20; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));
                MyPokemon mypokemon = new MyPokemon();
                mypokemon.name = pokemon.Name;
                mypokemon.image = pokemon.Sprites.FrontDefault;
                //Récupération des hp du pokémon
                mypokemon.hp = pokemon.Stats[0].BaseStat;
                //Récupération des points d'attaque du pokémon
                mypokemon.attaque = pokemon.Stats[1].BaseStat;
                //Récupération de la défense du pokémon
                mypokemon.defense = pokemon.Stats[2].BaseStat;
                ListOfPokemon.Add(mypokemon);
            }
        }

    }
}

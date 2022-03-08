using PokeApiNet;
using PokePhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public MyPokemon DernierPokemonTapper { get; private set; }

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
                //Récupération du type du pokémon
                mypokemon.type = pokemon.Types[0].Type.Name;
                //Récupération de l'id du pokémon
                mypokemon.id = pokemon.Id;
                //Récupération de la couleur du type du pokémon
                mypokemon.couleurType = ColoreFondPokemonSelonType(mypokemon.type);
                ListOfPokemon.Add(mypokemon);
            }
        }
        private Color ColoreFondPokemonSelonType(string typePokemon)
        {
            Dictionary<string, Color> listeCouleursTypePokemons = CreerDictionnaireTypeCouleurPokemons();
            return listeCouleursTypePokemons[typePokemon];
        }

        private Dictionary<string,Color> CreerDictionnaireTypeCouleurPokemons()
        {
            Dictionary<string, Color> listeCouleursTypePokemons = new Dictionary<string, Color>();
            listeCouleursTypePokemons.Add("fire", Color.Orange);
            listeCouleursTypePokemons.Add("ice", Color.LightBlue);
            listeCouleursTypePokemons.Add("water", Color.Blue);
            listeCouleursTypePokemons.Add("grass", Color.LightGreen);
            listeCouleursTypePokemons.Add("fighting", Color.Red);
            listeCouleursTypePokemons.Add("flying", Color.Purple);
            listeCouleursTypePokemons.Add("electric", Color.Yellow);
            listeCouleursTypePokemons.Add("ground", Color.RosyBrown);
            listeCouleursTypePokemons.Add("psychic", Color.Pink);
            listeCouleursTypePokemons.Add("rock", Color.Brown);
            listeCouleursTypePokemons.Add("bug", Color.Green);
            listeCouleursTypePokemons.Add("dragon", Color.DarkBlue);
            listeCouleursTypePokemons.Add("ghost", Color.DarkMagenta);
            listeCouleursTypePokemons.Add("dark", Color.DarkGray);
            listeCouleursTypePokemons.Add("steel", Color.LightGray);
            listeCouleursTypePokemons.Add("fairy", Color.LightPink);
            listeCouleursTypePokemons.Add("normal", Color.Khaki);

            return listeCouleursTypePokemons;
        }
    }
}

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
            listeCouleursTypePokemons.Add("fire", Color.FromRgb(254, 158, 84));
            listeCouleursTypePokemons.Add("ice", Color.FromRgb(117, 207, 193));
            listeCouleursTypePokemons.Add("water", Color.FromRgb(79, 146, 215));
            listeCouleursTypePokemons.Add("grass", Color.FromRgb(102, 189, 90));
            listeCouleursTypePokemons.Add("fighting", Color.FromRgb(207, 64, 108));
            listeCouleursTypePokemons.Add("flying", Color.FromRgb(143, 171, 223));
            listeCouleursTypePokemons.Add("electric", Color.FromRgb(244, 211, 58));
            listeCouleursTypePokemons.Add("ground", Color.FromRgb(218, 123, 67));
            listeCouleursTypePokemons.Add("psychic", Color.FromRgb(250, 115, 123));
            listeCouleursTypePokemons.Add("rock", Color.FromRgb(199, 185, 141));
            listeCouleursTypePokemons.Add("bug", Color.FromRgb(146, 194, 44));
            listeCouleursTypePokemons.Add("dragon", Color.FromRgb(6, 111, 196));
            listeCouleursTypePokemons.Add("ghost", Color.FromRgb(82, 107, 175));
            listeCouleursTypePokemons.Add("dark", Color.FromRgb(90, 84, 102));
            listeCouleursTypePokemons.Add("steel", Color.FromRgb(79, 139, 159));
            listeCouleursTypePokemons.Add("fairy", Color.FromRgb(237, 145, 230));
            listeCouleursTypePokemons.Add("normal", Color.FromRgb(146, 155, 164));

            return listeCouleursTypePokemons;
        }
    }
}

using PokeApiNet;
using PokePhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            InitListAsync();
        }
        public ObservableCollection<MyPokemon> ListOfPokemon { get; private set; }
        //Cette fonction ajoute les pokémons en base à une liste afficher par la suite dans la vue listePokemon
        protected void CreerListePokemonsViaBDD()
        {
            List<MyPokemon> listpokemonsBDD = App.BaseDeDonnees.GetPokemonsAsync().Result;
            foreach (MyPokemon pokemonBDD in listpokemonsBDD)
            {
                Debug.WriteLine("QZESRDTFGYUHIJOPO9O8TI7REYTFJHKHIOYTYUFTGHCNVJGHDYRETRTYDFJGKUHIUOY8T7IURFJHV?GFRTYTIUYOJLKHJGKFRUT");
                Debug.WriteLine("ajout de :" + pokemonBDD.Name);
                ListOfPokemon.Add(pokemonBDD);
            }
        }
        //Cette fonction ajoute les pokémons de l'API à une liste afficher par la suite dans la vue listePokemon
        protected async void CreerListePokemonViaAPI()
        {
            
            const int IndexPremierType = 0;
            const int IndexDeuxiemeType = 1;
            const int NombrePokemonPrisDansAPI = 50;
            PokeApiClient pokeClient = new PokeApiClient();
            //Ajout des pokémons de l'API dans une liste utilisée par l'application pour afficher les pokémons
            for (int i = 1; i <= NombrePokemonPrisDansAPI; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));
                MyPokemon mypokemon = new MyPokemon();
                mypokemon.Name = pokemon.Name;
                //Récupération des images du pokémon
                mypokemon.Image = pokemon.Sprites.FrontDefault;
                mypokemon.ImageFemelle = pokemon.Sprites.FrontFemale;
                mypokemon.ImageShiny = pokemon.Sprites.FrontShiny;
                //Récupération des hp du pokémon
                mypokemon.Hp = pokemon.Stats[0].BaseStat;
                //Récupération des points d'attaque du pokémon
                mypokemon.Attaque = pokemon.Stats[1].BaseStat;
                //Récupération de la défense du pokémon
                mypokemon.Defense = pokemon.Stats[2].BaseStat;
                //Récupération du type du pokémon
                mypokemon.Type = pokemon.Types[IndexPremierType].Type.Name;
                /*Cette ligne sert à vérifier que le pokémon à deux type avant d'essayer d'en ajouter un, 
                 * car sinon on essaye d'accéder à un élément innexistant*/
                if (VerifierPokemonADeuxTypes(pokemon))
                {
                    mypokemon.Type2 = pokemon.Types[IndexDeuxiemeType].Type.Name;
                }
                //Récupération de l'id du pokémon
                mypokemon.Id = pokemon.Id;
                //Récupération des abilités du pokémon
                mypokemon.Ability = pokemon.Abilities[0].Ability.Name;
                mypokemon.Gender = "Male";
                if (mypokemon.ImageFemelle != null)
                {
                    mypokemon.Gender += ", Female";
                }

                mypokemon.CouleurType = ColoreFondPokemonSelonType(mypokemon.Type);
                ListOfPokemon.Add(mypokemon);
                Debug.WriteLine("added " + mypokemon.Name+ " : i : " +i);
            }
            AjouterListePokemonEnBDD();
        }
        //Cette fonction vérifie qu'une pokémon à plusieurs type en testant la longueur du tableau contenant les types dans l'API
        private bool VerifierPokemonADeuxTypes(Pokemon pokemon)
        {
            return pokemon.Types.Count > 1;
        }

        private void AjouterListePokemonEnBDD()
        {
            //On ajoute les pokémons de l'API en BDD
            App.BaseDeDonnees.SauvegarderPokemons(ListOfPokemon.ToList());
        }

       
        public async Task InitListAsync()
        {
            //Si la base de données est remplis, les pokémons sont insérés dans une liste à partir de celle-ci
           // await App.BaseDeDonnees.NetoyerLaBDD();
            
            //Sinon, on part de l'API (on regarde si la liste est vide, si c'est le cas c'est que la BDD est vide (TODO : A VERIF !!!!)
            if (App.BaseDeDonnees.GetPokemonsAsync().Result.Count==0)
            {
                Debug.WriteLine("ON PASSE PAR l'API!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                CreerListePokemonViaAPI();
            } else
            {
                Debug.WriteLine("ON PASSE PAR LA BASE ???????????????????????????????????????????");
                CreerListePokemonsViaBDD();
            }
        }
        private String ColoreFondPokemonSelonType(string typePokemon)
        {
            Dictionary<string, String> listeCouleursTypePokemons = CreerDictionnaireTypeCouleurPokemons();
            return listeCouleursTypePokemons[typePokemon];
        }
        //TODO : mettre la définition ailleurs
        private Dictionary<string, String> CreerDictionnaireTypeCouleurPokemons()
        {
            Dictionary<string, String> listeCouleursTypePokemons = new Dictionary<string, String>();
            listeCouleursTypePokemons.Add("fire", "#fe9e54");
            listeCouleursTypePokemons.Add("ice", "#75cfc1");
            listeCouleursTypePokemons.Add("water", "#4f92d7");
            listeCouleursTypePokemons.Add("grass", "#66bd5a");
            listeCouleursTypePokemons.Add("fighting", "#cf406c");
            listeCouleursTypePokemons.Add("flying", "#8fabdf");
            listeCouleursTypePokemons.Add("electric", "#f4d33a");
            listeCouleursTypePokemons.Add("ground", "#da7b43");
            listeCouleursTypePokemons.Add("psychic", "#fa737b");
            listeCouleursTypePokemons.Add("rock", "#c7b98d");
            listeCouleursTypePokemons.Add("bug", "#92c22c");
            listeCouleursTypePokemons.Add("dragon", "#066fc4");
            listeCouleursTypePokemons.Add("ghost", "#526baf");
            listeCouleursTypePokemons.Add("dark", "#5a5466");
            listeCouleursTypePokemons.Add("steel", "#4f8b9f");
            listeCouleursTypePokemons.Add("fairy", "#ed91e6");
            listeCouleursTypePokemons.Add("normal", "#929ba4");
            listeCouleursTypePokemons.Add("poison", "#ab6cc9");
            return listeCouleursTypePokemons;
        }
    }
}
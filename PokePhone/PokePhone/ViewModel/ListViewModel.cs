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
    public class ListViewModel : BaseViewModel
    {

        private static ListViewModel _instance = new ListViewModel();


        public static ListViewModel Instance
        {
            get { return _instance; }
        }

        public ObservableCollection<MyPokemon> ListOfPokemon
        {
            get => GetValue<ObservableCollection<MyPokemon>>();
            set => SetValue(value);
        }

        public ListViewModel()
        {
            ListOfPokemon = new ObservableCollection<MyPokemon>();
            InitListAsync();
        }

        

        //public ObservableCollection<MyPokemon> ListOfPokemon { get; private set; }

        //Cette fonction ajoute les pokémons de la base de données à la liste affichée par la suite dans la vue listePokemon
        protected void CreerListePokemonsViaBDD()
        {
            List<MyPokemon> listpokemonsBDD = App.BaseDeDonnees.GetPokemonsAsync().Result;
            foreach (MyPokemon pokemonBDD in listpokemonsBDD)
            {
                Debug.WriteLine("ajout de :" + pokemonBDD.Name);
                if (pokemonBDD.Id<=50)
                {
                    ListOfPokemon.Add(pokemonBDD);
                } else
                {
                    ListOfPokemon.Insert(0, pokemonBDD);
                }
                
            }
        }


        //Cette fonction ajoute les pokémons de l'API à la liste affichée par la suite dans la vue listePokemon si la base de données est vide
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
                 * car sinon on essaye d'accéder à un élément inexistant*/
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


        //On ajoute les pokémons de l'API en BDD
        private void AjouterListePokemonEnBDD()
        {
            App.BaseDeDonnees.SauvegarderPokemons(ListOfPokemon.ToList());
        }

       
        public async Task InitListAsync()
        {
            //Si la base de données est remplis, les pokémons sont insérés dans une liste à partir de celle-ci, sinon on part de l'API
            //await App.BaseDeDonnees.NetoyerLaBDD();
            
            if (App.BaseDeDonnees.GetPokemonsAsync().Result.Count==0)
            {
                CreerListePokemonViaAPI();
            } else
            {
                CreerListePokemonsViaBDD();
            }
        }


        /*Récupère le couleur du pokemon en fonction de son type*/
        public String ColoreFondPokemonSelonType(string typePokemon)
        {
            Dictionary<string, String> listeCouleursTypePokemons = CreerDictionnaireTypeCouleurPokemons();
            return listeCouleursTypePokemons[typePokemon];
        }
        

        /*Dictionnaire associant les types des pokemons à leur couleur*/
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
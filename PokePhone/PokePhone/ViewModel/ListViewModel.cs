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

        //TODO : rendre ceci fonctionnelle pour afficher la liste des pokémons depuis la base de données (seulement OnAppearing()
        //Tuto : https://docs.microsoft.com/fr-fr/xamarin/get-started/tutorials/local-database/?tabs=vswin&tutorial-step=3
        /****added code****/
        /*
        protected override async void CreerListViaBDD()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }
        //TODO mixée cette fonction avec ajouter pokémon
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text)
                });

                nameEntry.Text = ageEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.BaseDeDonnees.GetPeopleAsync();
            }
        }
        /****added code****/

        public async void InitList()
        {

        PokeApiClient pokeClient = new PokeApiClient();
            //Ajout des pokémons de l'API dans une liste utiliser par l'application pour afficher les pokémons
            for (int i = 1; i <= 50; i++)
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
                mypokemon.Type = pokemon.Types[0].Type.Name;
                //Récupération de l'id du pokémon
                mypokemon.Id = pokemon.Id;
                //Récupération des abilités du pokémon
                mypokemon.Ability = pokemon.Abilities[0].Ability.Name;
                mypokemon.Gender = "Male";
                if (mypokemon.ImageFemelle != null)
                {
                    mypokemon.Gender += ", Female";
                }
                mypokemon.couleurType = ColoreFondPokemonSelonType(mypokemon.Type);
                ListOfPokemon.Add(mypokemon);
                App.BaseDeDonnees.SauvegarderPokemons(ListOfPokemon.ToList());
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

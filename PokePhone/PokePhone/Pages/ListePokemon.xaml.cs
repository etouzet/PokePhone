using PokePhone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokePhone.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListePokemon : ContentPage
    {
        Label nomPokemon = new Label();
        Label typePokemon = new Label();
        public ListePokemon()
        {
            InitializeComponent();
            BindingContext = new ListViewModel();

            var detecteurAppuisPokemon = new TapGestureRecognizer();
        }
        //Cette fonction gère l'affichage des pokémons
        async void AfficherPokemon(object sender, EventArgs eventArgs)
        {
            nomPokemon.Text = "Mettre ici le contenu du vrai nom, mais comment on le choppe?";
            ContentPage pageAfficagePoekmon = new ContentPage
            {
                BackgroundColor = FaireFondPagePokemonSelonType(),
                Content = new StackLayout
                {
                    Margin = 8,
                    Children =
                    {
                        nomPokemon,
                        typePokemon,
                    },
                }
            };
            
            await Navigation.PushAsync(pageAfficagePoekmon);
        }

        Color FaireFondPagePokemonSelonType()
        {
            //TODO : implémenter. Selon le type du pokémon, on change la couleur de fond de la page
            
            return Color.AliceBlue;
        }
    }
}
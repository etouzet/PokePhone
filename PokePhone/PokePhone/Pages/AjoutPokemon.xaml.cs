using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokePhone.ViewModel;
using PokePhone.Model;

namespace PokePhone.Pages 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjoutPokemon : ContentPage
    {
        
        public AjoutPokemon()
        {
            InitializeComponent();
        }
        /****added code****/
        //TODO : trouver un truc pour l'ID des pokémons, car je ne l'ai pas définie dans CreerPokemon()
        async void AjouterPokemonBDD(MyPokemon myPokemon)
        {
            await App.BaseDeDonnees.SauvegarderPokemon(myPokemon);
            //nameEntry.Text = ageEntry.Text = string.Empty;
            //collectionView.ItemsSource = await App.BaseDeDonnees.GetPeopleAsync();
        }
        /****added code****/

        public MyPokemon CreerPokemon()
        {
            MyPokemon nouveauPokemon = new MyPokemon();
            try
            {
                nouveauPokemon.Name = saisieNom.Text;
                nouveauPokemon.Type = saisieType.Items[saisieType.SelectedIndex];
                nouveauPokemon.Hp = Convert.ToInt32(saisieHp.Text);
                nouveauPokemon.Attaque = Convert.ToInt32(saisieAttack.Text);
                nouveauPokemon.Defense = Convert.ToInt32(saisieDefense.Text);
                nouveauPokemon.Image = Convert.ToString(saisieImage.Source);
            }
            catch (Exception)
            {
                throw new Exception("Une erreur est survenue durant l'ajout du pokémon");
            }

            return nouveauPokemon;
        }
        //TODO : Regler Franglais !
        public async void AjouterImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supporté", "Votre appareil ne supporte pas cette fonctionnalité", "Ok");
                return;
            }

            PickMediaOptions mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            saisieImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }

        public async void BtnAjouterPokemon(object sender, EventArgs args)
        {
            if (!ChampsSonRemplis())
            {
                saisieHp.PlaceholderColor = Color.Red;
                saisieAttack.PlaceholderColor = Color.Red;
                saisieDefense.PlaceholderColor = Color.Red;
                saisieNom.PlaceholderColor = Color.Red;
                saisieType.TitleColor = Color.Red;
                await DisplayAlert("Erreur", "Veuilliez remplier tous les champs", "Ok");
            }
            else
            {
                MyPokemon myPokemon = CreerPokemon();
                await DisplayAlert("Ajouter Pokémon", myPokemon.Name + " a été crée", "Ok");
                await DisplayAlert("Stat pokémon", myPokemon.VersChaine(), "Ok");
                //AjouterPokemonBDD(myPokemon);
                saisieHp.PlaceholderColor = Color.White;
                saisieAttack.PlaceholderColor = Color.White;
                saisieDefense.PlaceholderColor = Color.White;
                saisieNom.PlaceholderColor = Color.White;
                saisieType.TitleColor = Color.White;

                saisieImage.Source = "add.png";
                saisieHp.Text = null;
                saisieAttack.Text = null;
                saisieDefense.Text = null;
                saisieNom.Text = null;
                saisieType.SelectedItem = null ;
            }

        }

        private bool ChampsSonRemplis()
        {
            return saisieHp.Text != "" && saisieAttack.Text != "" 
                && saisieDefense.Text != "" && saisieNom.Text != "";
        }
    }
}
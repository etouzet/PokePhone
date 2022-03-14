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

        public MyPokemon CreerPokemon()
        {
            MyPokemon nouveauPokemon = new MyPokemon();
            try
            {
                nouveauPokemon.Name = this.saisieNom.Text;
                nouveauPokemon.Type = this.saisieType.Items[saisieType.SelectedIndex];
                nouveauPokemon.Hp = Convert.ToInt32(this.saisieHp.Text);
                nouveauPokemon.Attaque = Convert.ToInt32(this.saisieAttack.Text);
                nouveauPokemon.Defense = Convert.ToInt32(this.saisieDefense.Text);
                nouveauPokemon.Image = this.saisieImage.Source.ToString();
            }
            catch (Exception)
            {
                throw new Exception("Une erreur est survenue durant l'ajout du pokémon");
            }

            return nouveauPokemon;
        }
        //TODO : Regler Franglais !
        public async void AjouterImage(object sender, System.EventArgs e)
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

            this.saisieImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

        }

        public async void BtnAjouterPokemon(object sender, EventArgs args)
        {

            if (!ChampsSonRemplis())
            {
                this.saisieHp.PlaceholderColor = Color.Red;
                this.saisieAttack.PlaceholderColor = Color.Red;
                this.saisieDefense.PlaceholderColor = Color.Red;
                this.saisieNom.PlaceholderColor = Color.Red;
                this.saisieType.TitleColor = Color.Red;
                await DisplayAlert("Erreur", "Veuilliez remplier tous les champs", "Ok");
            }
            else
            {
                MyPokemon myPokemon = CreerPokemon();
                await DisplayAlert("Ajouter Pokémon", myPokemon.Name + " a été crée", "Ok");
                await DisplayAlert("Stat pokémon", myPokemon.VersChaine(), "Ok");
                //Il faut ensuite entrer le pokémon en BDD
            }

        }

        private bool ChampsSonRemplis()
        {
            return this.saisieHp.Text != "" && this.saisieAttack.Text != "" && this.saisieDefense.Text != "" && this.saisieNom.Text != "";
        }
    }
}
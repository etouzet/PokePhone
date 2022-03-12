using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AjoutPokemon : ContentPage
    {
        public AjoutPokemon()
        {
            InitializeComponent();
        }

        async void AjouterImage(object sender, System.EventArgs e)
        {
            
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supporté", "Votre appareil ne supporte pas cette fonctionnalité", "Ok");
                return;

            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            this.saisieImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

        }

        public void BtnAjouterPokemon(object sender, EventArgs args)
        {

            if (this.saisieHp.Text != "" || this.saisieAttack.Text != "" || this.saisieDefense.Text != "" || this.saisieNom.Text != "")
            {
                this.saisieHp.PlaceholderColor = Color.Red;
                this.saisieAttack.PlaceholderColor = Color.Red;
                this.saisieDefense.PlaceholderColor = Color.Red;
                this.saisieNom.PlaceholderColor = Color.Red;
                this.saisieType.TitleColor = Color.Red;

            }
            
        }

    }
}
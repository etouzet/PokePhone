﻿using Plugin.Media;
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
            //! added using Plugin.Media;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                return;

            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            // if you want to take a picture use TakePhotoAsync instead of PickPhotoAsync
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
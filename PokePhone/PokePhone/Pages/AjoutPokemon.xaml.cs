﻿using Plugin.Media;
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
        /*Cette variable sert à recupérer la dernière image récupérée, car le type de ".Source" de l'image button n'as pas l'air de permettre
         une récupération de l'url*/
        private string urlImagePokemon = "";
        public AjoutPokemon()
        {
            InitializeComponent();
        }
        /****added code****/
        //TODO : trouver un truc pour l'ID des pokémons, car je ne l'ai pas définie dans CreerPokemon()
        async void AjouterPokemonBDD(MyPokemon myPokemon)
        {
            /*On utilise un type intermédiaire pour la base de données d'un pokémon sans le champs couleurType*/
            await App.BaseDeDonnees.SauvegarderPokemon(myPokemon);
        }
        /****added code****/

        public MyPokemon CreerPokemon()
        {
            MyPokemon nouveauPokemon = new MyPokemon();
            nouveauPokemon.Id = App.BaseDeDonnees.GetPokemonsAsync().Result.Count + 1;
                nouveauPokemon.Name = saisieNom.Text;
                nouveauPokemon.Type = saisieType.Items[saisieType.SelectedIndex];
            if (saisieType2.SelectedItem == null)
            {
                nouveauPokemon.Type2 = null; 
            } else
            {
                nouveauPokemon.Type2 = saisieType2.Items[saisieType2.SelectedIndex];
            }
                nouveauPokemon.Hp = Convert.ToInt32(saisieHp.Text);
                nouveauPokemon.Attaque = Convert.ToInt32(saisieAttack.Text);
                nouveauPokemon.Defense = Convert.ToInt32(saisieDefense.Text);
                //nouveauPokemon.CouleurType = "#fe9e54";
                nouveauPokemon.CouleurType = ListViewModel.Instance.ColoreFondPokemonSelonType(nouveauPokemon.Type);
                nouveauPokemon.Gender = "Male";
                //var stream = saisieImage.Source.GetValue(StreamImageSource.StreamProperty);
                nouveauPokemon.Image = urlImagePokemon; /*Convert.ToString(stream.ToString());*/
                ListViewModel.Instance.ListOfPokemon.Insert(0, nouveauPokemon);
                

            return nouveauPokemon;
        }
        //TODO : Regler Franglais !
        public async void AjouterImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (ajoutImageNonSuporte())
            {
                await DisplayAlert("Non supporté", "Votre appareil ne supporte pas cette fonctionnalité", "Ok");
                return;
            }

            PickMediaOptions optionsDuMedia = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var fichierImageSelectionne = await CrossMedia.Current.PickPhotoAsync(optionsDuMedia);
            if(fichierImageSelectionne != null)
            {
                saisieImage.Source = ImageSource.FromStream(() => fichierImageSelectionne.GetStream());
            } else
            {
                saisieImage.Source = "add.png";
            }
            
            urlImagePokemon = fichierImageSelectionne.Path;
        }

        private static bool ajoutImageNonSuporte()
        {
            return !CrossMedia.Current.IsPickPhotoSupported;
        }

        public async void BtnAjouterPokemon(object sender, EventArgs args)
        {
            if (!ChampsSontRemplis())
            {
                saisieHp.PlaceholderColor = Color.Red;
                saisieAttack.PlaceholderColor = Color.Red;
                saisieDefense.PlaceholderColor = Color.Red;
                saisieNom.PlaceholderColor = Color.Red;
                saisieType.TitleColor = Color.Red;

                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "Ok");

            }
            else
            {
                MyPokemon myPokemon = CreerPokemon();
                await DisplayAlert("Ajouter Pokémon", myPokemon.Name + " a été crée", "Ok");
                await DisplayAlert("Stat pokémon", myPokemon.VersChaine(), "Ok");
                AjouterPokemonBDD(myPokemon); 
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
                urlImagePokemon = "";
            }

        }
        private bool ChampsSontRemplis()
        {
            return (saisieHp.Text != "") && (saisieAttack.Text != "") 
                && (saisieDefense.Text != "") && (saisieNom.Text != "")
                && (urlImagePokemon != "");
        }
    }


}


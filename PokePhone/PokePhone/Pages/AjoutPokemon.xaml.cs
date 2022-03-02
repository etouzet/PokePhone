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

        public void BtnAjouterPokemon(object sender, EventArgs args)
        {
            if (this.saisieHp.Text != "")
            {
                this.saisieHp.PlaceholderColor = Color.Red;
            }
        }
    }
}
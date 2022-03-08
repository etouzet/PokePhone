using PokePhone.Model;
using PokePhone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokePhone.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListePokemon : ContentPage
    {
        public ListePokemon()
        {
            InitializeComponent();
            BindingContext = new ListViewModel();
        }

        void OnClick(object sender, ItemTappedEventArgs e)
        {
            MyPokemon current = (e.Item as MyPokemon);
            Navigation.PushAsync(new AffichagePokemon(current));
        }
    }
}
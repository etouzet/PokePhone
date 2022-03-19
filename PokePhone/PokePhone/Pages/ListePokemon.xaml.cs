using PokePhone.Model;
using PokePhone.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        ListViewModel listViewModel = ListViewModel.Instance;
        public ListePokemon()
        {
            InitializeComponent();
            BindingContext = listViewModel;
        }

        void OnClick(object sender, ItemTappedEventArgs e)
        {
            MyPokemon current = (e.Item as MyPokemon);
            Navigation.PushAsync(new AffichagePokemon(current));
        }
    }
}
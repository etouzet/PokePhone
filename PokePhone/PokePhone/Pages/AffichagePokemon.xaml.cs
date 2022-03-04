﻿using PokePhone.ViewModel;
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
    public partial class AffichagePokemon : ContentView
    {
        public AffichagePokemon()
        {
            InitializeComponent();
            BindingContext = new AffichagePokemonViewModel();
        }
    }
}
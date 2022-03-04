﻿using PokePhone.ViewModel;
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
    }
}
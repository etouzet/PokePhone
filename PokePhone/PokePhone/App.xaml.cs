using PokePhone.Model;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokePhone
{
    public partial class App : Application
    {

        private static BaseDeDonnees baseDeDonnees;

        public static BaseDeDonnees BaseDeDonnees
        {
            get
            {
                if (baseDeDonnees == null)
                {
                    baseDeDonnees = new BaseDeDonnees(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyPokemon.db"));
                }
                return baseDeDonnees;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

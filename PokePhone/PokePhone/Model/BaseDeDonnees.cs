using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PokePhone.Model
{
    public class BaseDeDonnees
    {
        readonly SQLiteAsyncConnection _baseDeDonnees;

        public BaseDeDonnees(string cheminBDD)
        {
            _baseDeDonnees = new SQLiteAsyncConnection(cheminBDD);
            _baseDeDonnees.CreateTableAsync<MyPokemon>().Wait();
        }

        public Task<List<MyPokemon>> GetPokemonAsync()
        {
            return _baseDeDonnees.Table<MyPokemon>().ToListAsync();
        }

        public Task<int> SauvegarderPokemon(MyPokemon myPokemon)
        {
            return _baseDeDonnees.InsertAsync(myPokemon);
        }

        internal Task GetPeopleAsync()
        {
            throw new NotImplementedException();
        }
    }
}

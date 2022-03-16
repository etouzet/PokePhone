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

        public Task<List<MyPokemon>> GetPokemonsAsync()
        {
            return _baseDeDonnees.Table<MyPokemon>().ToListAsync();
        }

        public Task<MyPokemon> GetPokemonsAsync(int id)
        {
            return _baseDeDonnees.Table<MyPokemon>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SauvegarderPokemon(MyPokemon myPokemon)
        {
            return _baseDeDonnees.InsertAsync(myPokemon);
        }

        //Insère une liste de pokémons en base de données. A voir si c'est utile
        public void SauvegarderPokemons(List<MyPokemon> myPokemons)
        {
            foreach (MyPokemon myPokemon in myPokemons)
            {
                SauvegarderPokemon(myPokemon);
            }
        }
    }
}

// Services/DatabaseService.cs
using System;
using System.IO;
using SQLite;
using projDevMain.Models;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace projDevMain.Services
{
    public class DatabaseService
    {

        private readonly SQLiteAsyncConnection _connection;

        //NAMING THE DATABASE IS SET TO READONLY
        private static readonly Lazy<SQLiteConnection> lazyInitializer = new Lazy<SQLiteConnection>(() =>
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db3");
            return new SQLiteConnection(path);
        });

        private static SQLiteConnection Database => lazyInitializer.Value;
        private static bool initialized = false;
        //INITIALIZE DATABASE
        public DatabaseService()
        {
            InitializeDatabase();
        }
        //MAKING CONNECTION FROM DATABASE TO GAMELISTMODEL
        public DatabaseService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<GameListModel>();
        }
        //ADD GAME TASK FOR DATABASE
        public Task<int> addGame(GameListModel model)
        {

            return _connection.InsertAsync(model);
        }
        //RETRIVES THE GAME DATAS FROM DATABASE 
        public Task<List<GameListModel>> getGameList()
        {

            return _connection.Table<GameListModel>().ToListAsync();
        }
        //UPDATE THE DATABASE FOR CHANGES 
        public Task<int> updateGame(GameListModel game)
        {

            return _connection.UpdateAsync(game);
        }
        //DELETE GAME FROM THE DATABASE
        public Task<int> deleteGame(GameListModel game)
        {

            return _connection.DeleteAsync(game);
        }
        //SEARCH FUNCTION FOR GAME IN DATABASE
        public Task<List<GameListModel>> Search(string search)
        {

            return _connection.Table<GameListModel>().Where(p => p.Name.StartsWith(search)).ToListAsync();
        }





        private void InitializeDatabase()
        {
            if (!initialized)
            {
                Database.CreateTable<User>();
                initialized = true;
            }
        }

        public int SaveUser(User user)
        {
            return Database.Insert(user);
        }
        public int UpdateUser(User user) { return Database.Update(user); }

        public User GetUser(string username, string password)
        {
            return Database.Table<User>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}

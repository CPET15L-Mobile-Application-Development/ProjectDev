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
        private readonly SQLiteAsyncConnection _connection;     // SETS THE SQLLITE CONNECTION IN READONLY

        //MAKING THE SQL DATABASE IN LOCAL STORAGE
        private static readonly Lazy<SQLiteConnection> lazyInitializer = new Lazy<SQLiteConnection>(() =>
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db3");
            return new SQLiteConnection(path);
        });

        private static SQLiteConnection Database => lazyInitializer.Value;      
        private static bool initialized = false;

        public DatabaseService()
        {
            InitializeDatabase();
        }

        //CONNECTION BETWEEN SQL AND MODELS
        public DatabaseService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<GameListModel>();
        }
        // ADD GAME FUNCTION FOR DATABASE
        public Task<int> addGame(GameListModel model)
        {
            return _connection.InsertAsync(model);
        }
        //GETS THE GAMELIST FROM DATABASE
        public Task<List<GameListModel>> getGameList()
        {
            return _connection.Table<GameListModel>().ToListAsync();
        }
        //UPDATE GAME INFO IN DATABASE
        public Task<int> updateGame(GameListModel game)
        {
            return _connection.UpdateAsync(game);
        }
        //DELETE GAME FROM THE DATABASE
        public Task<int> deleteGame(GameListModel game)
        {
            return _connection.DeleteAsync(game);
        }
        //SEARCH GAMES IN THE DATABASE
        public Task<List<GameListModel>> Search(string search)
        {
            return _connection.Table<GameListModel>().Where(p => p.Name.StartsWith(search)).ToListAsync();
        }
        //CREATES DATABASE TABLE FOR USERS
        private void InitializeDatabase()
        {
            if (!initialized)
            {
                Database.CreateTable<User>();
                initialized = true;
            }
        }
        //SAVES NEW USER
        public int SaveUser(User user)
        {
            return Database.Insert(user);
        }
        //UPDATE NEW USERS INFO
        public int UpdateUser(User user)
        {
            return Database.Update(user);
        }
        //GET USERS INFO
        public User GetUser(string username, string password)
        {
            return Database.Table<User>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }
        //GET USERS BY ID FOR ACCOUNT SESSION
        public User GetUserById(int userId)
        {
            return Database.Table<User>().FirstOrDefault(u => u.Id == userId);
        }
    }
}

// Services/DatabaseService.cs
using System;
using System.IO;
using SQLite;
using projDevMain.Models;
using Xamarin.Forms;

namespace projDevMain.Services
{
    public class DatabaseService
    {
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

        public User GetUser(string username, string password)
        {
            return Database.Table<User>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}

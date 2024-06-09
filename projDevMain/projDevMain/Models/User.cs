// Models/User.cs
using SQLite;

namespace projDevMain.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        // Add other user details if necessary
    }
}
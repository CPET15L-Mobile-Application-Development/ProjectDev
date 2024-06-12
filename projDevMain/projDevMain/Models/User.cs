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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string JobCaption { get; set; }
        public string Wallpaper { get; set; }
        public string ProfilePicture { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }

        public string Image1 { get; set; }      //FOR STORING IMAGES
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
    }
}
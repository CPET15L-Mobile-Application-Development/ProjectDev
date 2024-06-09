using SQLite;

namespace projDevMain.Models
{
    public class GameListModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public string Tags { get; set; } // Tags stored as comma-separated values
    }
}

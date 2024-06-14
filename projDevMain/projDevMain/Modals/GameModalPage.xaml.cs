using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Models;
using projDevMain.Services;
using System;
using Xamarin.Essentials;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameModalPage : ContentPage
    {
        private GameListModel game;
        private bool _isEdit;
        private DatabaseService _databaseService;

        public GameModalPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        Models.GameListModel _game;

        public GameModalPage(Models.GameListModel game)
        {
            InitializeComponent();
            _isEdit = true;
            Title = "Edit Game Information";
            _game = game;
            nameEntry.Text = _game.Name;
            imageEntry.Text = _game.Image;
            priceEntry.Text = _game.Price;
            ratingEntry.Text = _game.Rating;
            descripEntry.Text = _game.Description;
            SetTags(_game.Tags);
            nameEntry.Focus();
        }

        private void SetTags(string tags)
        {
            var tagList = tags.Split(new[] { "+", "," }, StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim()).ToList();
            tagAction.IsChecked = tagList.Contains("Action");
            tagAdventure.IsChecked = tagList.Contains("Adventure");
            tagRPG.IsChecked = tagList.Contains("Role-playing");
            tagSimulation.IsChecked = tagList.Contains("Simulation");
            tagStrategy.IsChecked = tagList.Contains("Strategy");
            tagSports.IsChecked = tagList.Contains("Sports");
            tagPuzzle.IsChecked = tagList.Contains("Puzzle");
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                string.IsNullOrWhiteSpace(priceEntry.Text) ||
                string.IsNullOrWhiteSpace(imageEntry.Text) ||
                string.IsNullOrWhiteSpace(ratingEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank or Whitespace is Invalid!", "OK");
            }
            else if (_game != null)
            {
                updateGame();
            }
            else
            {
                addGame();
            }
        }

        private string GetSelectedTags()
        {
            var selectedTags = new[]
            {
                tagAction.IsChecked ? "Action" : null,
                tagAdventure.IsChecked ? "Adventure" : null,
                tagRPG.IsChecked ? "Role-playing" : null,
                tagSimulation.IsChecked ? "Simulation" : null,
                tagStrategy.IsChecked ? "Strategy" : null,
                tagSports.IsChecked ? "Sports" : null,
                tagPuzzle.IsChecked ? "Puzzle" : null
            }.Where(tag => tag != null);

            return string.Join("+", selectedTags);
        }

        async void addGame()
        {
            await App.Service.addGame(new Models.GameListModel()
            {
                Name = nameEntry.Text,
                Image = imageEntry.Text,
                Price = priceEntry.Text,
                Rating = ratingEntry.Text,
                Tags = GetSelectedTags(),
                Description = descripEntry.Text,
            });
            await Navigation.PopAsync();
        }

        async void updateGame()
        {
            _game.Name = nameEntry.Text;
            _game.Image = imageEntry.Text;
            _game.Price = priceEntry.Text;
            _game.Rating = ratingEntry.Text;
            _game.Tags = GetSelectedTags();
            _game.Description = descripEntry.Text;

            await App.Service.updateGame(_game);
            await Navigation.PopAsync();
        }

        async void OnImportImageClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select an image file"
                });
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var filePath = await SaveFileToLocalStorage(result.FileName, stream);
                    imageEntry.Text = filePath;
                    gameImage.Source = ImageSource.FromFile(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }

        private async Task<string> SaveImageToLocalStorage(string imageUrl)
        {
            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                var webClient = new System.Net.WebClient();
                var imageBytes = await webClient.DownloadDataTaskAsync(new Uri(imageUrl));
                var filePath = Path.Combine(FileSystem.AppDataDirectory, Path.GetFileName(imageUrl));
                File.WriteAllBytes(filePath, imageBytes);
                return filePath;
            }
            return imageUrl;
        }

        private async Task<string> SaveFileToLocalStorage(string fileName, Stream fileStream)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }
    }
}

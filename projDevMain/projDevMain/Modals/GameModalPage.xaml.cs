using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Models;
using projDevMain.Services;
using System;
using Xamarin.Essentials;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameModalPage : ContentPage
    {

        private GameListModel game;
        private bool _isEdit;
        private DatabaseService _databaseService;


        //TAG FUNCTIONS PORTIONS
        //private ObservableCollection<string> _tags;
        //public ObservableCollection<string> Tags
        //{
        //    get => _tags;
        //    set
        //    {
        //        _tags = value;
        //        OnPropertyChanged(nameof(Tags));
        //    }
        //}
        //public ObservableCollection<string> SelectedTags { get; set; } = new ObservableCollection<string>();


        //MAIN GAMEMODAL PAGE INITIALIZE COMPONENTS
        public GameModalPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            //INITIALIZE THE TAGS
            //BindingContext = this;

            //Tags = new ObservableCollection<string>
            //{
            //    "Paid Games","Free Games","Action", "Adventure", "Role Playing", "Simulation", "Strategy", "Sports","Puzzle" //ADD MORE TAGS IF NEEDED
            //};

        }

        Models.GameListModel _game; 

        //INITIZALIZE DATABASE ATTRIBUTES
        public GameModalPage(Models.GameListModel game) {

            InitializeComponent();
            _isEdit = true;
            Title = "Edit Game Information";
            _game = game;
            nameEntry.Text = _game.Name;
            imageEntry.Text = _game.Image;
            priceEntry.Text = _game.Price;
            ratingEntry.Text = _game.Rating;
            descripEntry.Text = _game.Description;
            tagsEntry.Text = _game.Tags;
            nameEntry.Focus();

        }

       async void OnSaveClicked(object sender, EventArgs e)
        {
                //CHECKS FIELDS IF FILLED
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || 
                string.IsNullOrWhiteSpace(priceEntry.Text) ||
                string.IsNullOrWhiteSpace(imageEntry.Text) ||
                string.IsNullOrWhiteSpace(ratingEntry.Text)|| 
                string.IsNullOrWhiteSpace(tagsEntry.Text))
            { 
                await DisplayAlert("Invalid","Blank or Whitespace is Invalid!","OK");
            } else if (_game != null)
            {
                updateGame();
            }
            else
            {
                addGame();
            }
        }
        //ADD GAME DETAILS TO DATABASE
        async void addGame()
        {
            await App.Service.addGame(new Models.GameListModel()
            {
                Name = nameEntry.Text,
                Image = imageEntry.Text,
                Price = priceEntry.Text,
                Rating = ratingEntry.Text,
                Tags = tagsEntry.Text,
                Description = descripEntry.Text,

            });
            await Navigation.PopAsync(); 
        }
        //UPDATE GAME DETAILS IN DATABASE
        async void updateGame()
        {
            _game.Name = nameEntry.Text;
            _game.Image = imageEntry.Text;
            _game.Price = priceEntry.Text;
            _game.Rating = ratingEntry.Text;
            _game.Tags = tagsEntry.Text; //MUST BE UPDATED AND CHANGED
            _game.Description = descripEntry.Text;

            await App.Service.updateGame(_game);
            await Navigation.PopAsync();
        }

        //IMAGE IMPORT BUTTON LOCALLY OR URL
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
            } catch (Exception ex){

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
        //SAVE IMAGE LOCALLY FOR FASTER RETRVIEVE
        private async Task<string> SaveFileToLocalStorage(string fileName, Stream fileStream)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }

        //TAGS CHECKLIST FUNCTION DEFINES
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private void OnTagSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var tag = e.SelectedItem as string;
        //    if (tag != null && !SelectedTags.Contains(tag))
        //    {
        //        SelectedTags.Add(tag);
        //    }
        //    else if (tag != null && SelectedTags.Contains(tag))
        //    {
        //        SelectedTags.Remove(tag);
        //    }
        //}
    }
}

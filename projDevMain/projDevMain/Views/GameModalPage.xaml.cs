using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Models;
using projDevMain.Services;
using System;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameModalPage : ContentPage
    {
        

        public GameModalPage()
        {
            InitializeComponent();
            
        }

        Models.GameListModel _game;
        public GameModalPage(Models.GameListModel game) {

            InitializeComponent();
            Title = "Edit Game Information";
            _game = game;
            nameEntry.Text = _game.Name;
            imageEntry.Text = _game.Image;
            priceEntry.Text = _game.Price;
            ratingEntry.Text = _game.Rating;
            tagsEntry.Text = _game.Tags;
            nameEntry.Focus();

        }

       async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || (string.IsNullOrWhiteSpace(priceEntry.Text)||(string.IsNullOrWhiteSpace(imageEntry.Text)||(string.IsNullOrWhiteSpace(ratingEntry.Text)||(string.IsNullOrWhiteSpace(tagsEntry.Text))))))
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

        async void addGame()
        {
            await App.Service.addGame(new Models.GameListModel()
            {
                Name = nameEntry.Text,
                Image = imageEntry.Text,
                Price = priceEntry.Text,
                Rating = ratingEntry.Text,
                Tags = tagsEntry.Text,

            });
            await Navigation.PopAsync();
        }
        async void updateGame()
        {
            _game.Name = nameEntry.Text;
            _game.Image = imageEntry.Text;
            _game.Price = priceEntry.Text;
            _game.Rating = ratingEntry.Text;
            _game.Tags = tagsEntry.Text;

            await App.Service.updateGame(_game);
            await Navigation.PopAsync();
        }
    }
}

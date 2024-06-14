using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using projDevMain.Modals;
using projDevMain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class homePage : ContentPage
	{
        private User currentUser;

        // ObservableCollection<GameListModel> gamelist;
        public homePage(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Set the greeting label and profile picture
            greetingLabel.Text = $"Welcome, {user.Username}!";
            dp.Source = string.IsNullOrEmpty(user.ProfilePicture) ? "user.png" : ImageSource.FromFile(user.ProfilePicture);

            // Subscribe to the message for profile update
            MessagingCenter.Subscribe<accountPage, User>(this, "ProfileUpdated", (sender, updatedUser) =>
            {
                currentUser = updatedUser;
                UpdateUserProfile(currentUser);
            });

            // Subscribe to the message for image update
            MessagingCenter.Subscribe<ImageInfoModalPage, User>(this, "ProfileUpdated", (sender, updatedUser) =>
            {
                currentUser = updatedUser;
                UpdateUserProfile(currentUser);
            });
        }

        private void UpdateUserProfile(User user)
        {
            dp.Source = string.IsNullOrEmpty(user.ProfilePicture) ? "user.png" : ImageSource.FromFile(user.ProfilePicture);
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                gameDataView.ItemsSource = await App.Service.getGameList();

            }
            catch (Exception ex) { }
        }

        //UPON LOGGININ THE USERNAME ADAPTS TO HOMEPAGE USER LABEL
        public void SetUsername(string username)
        {
            var greetingLabel = this.FindByName<Label>("greetingLabel");
            greetingLabel.Text = $"Welcome, {username}!";

        }

        //BRIEF SUMMARY OF PROFILE IN A MODAL FUNCTION
        private async void profileModal(object sender, EventArgs e)
        {
            // Pass the current user details to the ProfileModalPage
            var profilePage = new ProfileModalPage(currentUser);
            await Navigation.PushModalAsync(profilePage);
        }
        //ADD ITEM GAME MODAL FUNCTION
        private async void clickAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameModalPage());
        }


        private void UpdateUserDetails(User user)
        {
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {

                dp.Source = ImageSource.FromFile(user.ProfilePicture);
            }
            
        }


        



        private List<GameListModel> games;
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }

            var searchTerm = e.NewTextValue?.ToLower();
            var filteredGames = games.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            gameDataView.ItemsSource = filteredGames;
            noResultsLabel.IsVisible = !filteredGames.Any();
        }



        private async void paidButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }

            var filteredgames = games.Where(p => double.TryParse(p.Price, out double price) && price > 0).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void allgames_Clicked(object sender, EventArgs e)
        {

            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            gameDataView.ItemsSource = games;
        }

        private async void freeButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }

            var filteredgames = games.Where(p => double.TryParse(p.Price, out double price) && price == 0).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void actionButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Action")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void stratButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Strategy")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void advButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Adventure")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void rpgButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Role-playing")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void simulButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Simulation")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void sportButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Sports")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void puzzleButton_Clicked(object sender, EventArgs e)
        {
            if (games == null)
            {
                games = await App.Service.getGameList();
            }
            var filteredgames = games.Where(p => p.Tags != null && p.Tags.Contains("Puzzle")).ToList();
            gameDataView.ItemsSource = filteredgames;
        }

        private async void descpGame_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var selectedGame = button.BindingContext as GameListModel;
            if (selectedGame != null)
            {
                await Navigation.PushModalAsync(new GameDescripModalPage(selectedGame));
            }
        }
    }
}
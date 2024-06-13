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
        public homePage (User user)
		{
            InitializeComponent();
            currentUser = user;
            var greetingLabel = this.FindByName<Label>("greetingLabel");
            greetingLabel.Text = $"Welcome, {user.Username}!";

            if ( !string.IsNullOrEmpty(user.ProfilePicture))
            {
                
                dp.Source = ImageSource.FromFile(user.ProfilePicture);
            }


            /*
                //STATIC POPULATE DATA
                //NOTE: BETTER IF THERE IS AN IMPLEMENTATION OF SQL
                gamelist = new ObservableCollection<GameListModel>
                {
                    //new GameListModel{Name = "", Image = "", Rating = ""}, POPULATE TEMPLATE
                    //FOR BETTER RESULTS TRY TO CONNECT TO THE SQL DATABASE
                    //PASS FROM SQL CONVERT THEN IMPORT TO THE NAME="" AND IMAGE=""

                    new GameListModel{Name = "Elden Ring", Image = "gameER.png", Rating = "8.8/10", Price="₱2,399.00"},
                    new GameListModel{Name = "Grand Theft Auto", Image = "gameGTA.png", Rating = "8.8/10", Price="₱655.18"},
                    new GameListModel{Name = "The Witcher - Wild Hunt", Image = "gameTW.png", Rating = "9.3/10", Price="₱1,699.00"},
                    new GameListModel{Name = "Red Dead Redemption", Image = "gameRDR.png", Rating = "8.9/10", Price = "₱920.00"},
                    new GameListModel{Name = "Minecraft", Image = "gameMC.png", Rating = "8.3/10" , Price = "₱925.00"},
                    new GameListModel{Name = "League of Legends", Image = "gameLOL.png", Rating = "7.5/10" , Price = "Free"},
                    new GameListModel{Name = "Fortnite", Image = "gameFN.png", Rating = "7.4/10" , Price = "Free"},
                    new GameListModel{Name = "Call of Duty", Image = "gameCOD.png", Rating = "7.8/10" , Price = "Free"},
                    new GameListModel{Name = "The Sims 4", Image = "gameS4.png", Rating = "7.2/10" , Price = "Free"},
                    new GameListModel{Name = "Apex Legends", Image = "gameAL.png", Rating = "8.5/10" , Price = "Free"},
                    new GameListModel{Name = "God of War", Image = "gameGOW.png", Rating = "8.7/10" , Price = "₱2,490.00"},
                    new GameListModel{Name = "Stardew Valley", Image = "gameSDV.png", Rating = "9.0/10" , Price = "₱419.95"},
                    new GameListModel{Name = "Hades", Image = "gameHD.png", Rating = "7.5/10" , Price = "₱765.00"},
                };

                //PASS THE DATA FROM THE DEFINED "gamelist" TO THE x:Name="gameCollectionView"
                gameDataView.ItemsSource = gamelist;*/
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
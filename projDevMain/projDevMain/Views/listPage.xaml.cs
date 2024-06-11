using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projDevMain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listPage : ContentPage
    {
        // ObservableCollection<GameListModel> gamelist;
        public listPage()
        {
            InitializeComponent();

            /*
            //STATIC POPULATE DATA
            //NOTE: BETTER IF THERE IS AN IMPLEMENTATION OF SQL
            gamelist = new ObservableCollection<GameListModel>
            {
                //new GameListModel{Name = "", Image = "", Rating = ""}, POPULATE TEMPLATE
                //FOR BETTER RESULTS TRY TO CONNECT TO THE SQL DATABASE
                //PASS FROM SQL CONVERT THEN IMPORT TO THE NAME="" AND IMAGE=""

                new GameListModel{Name = "Elden Ring", Image = "gameER.png", Price="₱2,399.00"},
                new GameListModel{Name = "Grand Theft Auto", Image = "gameGTA.png", Price="₱655.18"},
                new GameListModel{Name = "The Witcher - Wild Hunt", Image = "gameTW.png", Price="₱1,699.00"},
                new GameListModel{Name = "Red Dead Redemption", Image = "gameRDR.png", Price = "₱920.00"},
                new GameListModel{Name = "Minecraft", Image = "gameMC.png" , Price = "₱925.00"},
                new GameListModel{Name = "League of Legends", Image = "gameLOL.png", Price = "Free"},
                new GameListModel{Name = "Fortnite", Image = "gameFN.png", Price = "Free"},
                new GameListModel{Name = "Call of Duty", Image = "gameCOD.png", Price = "Free"},
                new GameListModel{Name = "The Sims 4", Image = "gameS4.png", Price = "Free"},
                new GameListModel{Name = "Apex Legends", Image = "gameAL.png", Price = "Free"},
                new GameListModel{Name = "God of War", Image = "gameGOW.png", Price = "₱2,490.00"},
                new GameListModel{Name = "Stardew Valley", Image = "gameSDV.png" , Price = "₱419.95"},
                new GameListModel{Name = "Hades", Image = "gameHD.png", Price = "₱765.00"},
            };

            //PASS THE DATA FROM THE DEFINED "gamelist" TO THE x:Name="gameCollectionView"
            gameDataView.ItemsSource = gamelist; */

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


        //SWIPEITEM EDIT GAME FUNCTION
        private async void editItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var game = item.CommandParameter as GameListModel;
            await Navigation.PushAsync(new GameModalPage(game));
        }
        //SWIPE ITEM DELETE GAME FUNCTION
        private async void delItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var game = item.CommandParameter as GameListModel;
            var result = await DisplayAlert("Delete", $"Delete {game.Name} from the database", "Yes", "No");
            if (result)
            {
                await App.Service.deleteGame(game);
                gameDataView.ItemsSource = await App.Service.getGameList();
            }
        }
        //ADD BUTTON NAVIGATE TO GAME MODAL PAGE
        private async void clickAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameModalPage());
        }
        //SEARCH THE DATABASE FROM THE SEARCH BAR STRING
        private List<GameListModel> allGames;
        private async void searchbar_changed(object sender, TextChangedEventArgs e)
        {
            if (allGames == null)
            {
                allGames = await App.Service.getGameList();
            }

            var searchTerm = e.NewTextValue?.ToLower();
            var filteredGames = allGames.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();
            gameDataView.ItemsSource = filteredGames;
        }
    }
}
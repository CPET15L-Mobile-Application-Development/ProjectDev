using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using projDevMain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class homePage : ContentPage
	{
        ObservableCollection<GameListModel> gamelist;
        public homePage (string username)
		{
			InitializeComponent ();
            // Set the greeting label
            var greetingLabel = this.FindByName<Label>("greetingLabel");
            greetingLabel.Text = $"Welcome, {username}!";


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
            gameDataView.ItemsSource = gamelist;
        }
        public void SetUsername(string username)
        {
            var greetingLabel = this.FindByName<Label>("greetingLabel");
            greetingLabel.Text = $"Welcome, {username}!";
        }

    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listPage : ContentPage
    {
        ObservableCollection<GameListModel> gamelist;
        public listPage()
        {
            InitializeComponent();

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
            gameDataView.ItemsSource = gamelist;

        }
    }
}
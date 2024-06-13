using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Models;

namespace projDevMain.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDescripModalPage : ContentPage
    {
        public GameDescripModalPage(GameListModel game)
        {
            InitializeComponent();
            BindingContext = game;
        }
    }
}

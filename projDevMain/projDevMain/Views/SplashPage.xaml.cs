using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projDevMain.Views;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            // Start the splash screen timeout
            StartSplashScreen();
        }

        private async void StartSplashScreen()
        {
            // Delay for 3 seconds
            await Task.Delay(3000);
            // Navigate to the login page
            Application.Current.MainPage = new NavigationPage(new loginPage());
        }
    }
}
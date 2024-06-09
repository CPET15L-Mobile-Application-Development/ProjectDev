using projDevMain.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace projDevMain
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the SplashScreen as the initial page
            MainPage = new SplashPage();
           // MainPage = new MainPage();


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

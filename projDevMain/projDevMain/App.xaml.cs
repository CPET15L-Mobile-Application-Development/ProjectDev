using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new registerPage();
            //MainPage = new loginPage();
            //MainPage = new NavigationPage(new loginPage());
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

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//EXPORT THE CUSTOM FONTS
[assembly: ExportFont("Gilroy-Bold.ttf", Alias ="Gilroy-Bold") ]
[assembly: ExportFont("Gilroy-Heavy.ttf", Alias ="Gilroy-Heavy") ]
[assembly: ExportFont("Gilroy-Light.ttf", Alias ="Gilroy-Light") ]
[assembly: ExportFont("Gilroy-Medium.ttf", Alias ="Gilroy-Medium") ]
[assembly: ExportFont("Gilroy-Regular.ttf", Alias ="Gilroy-Regular") ]

namespace projDevMain
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new loginPage();
            MainPage = new NavigationPage(new MainPage());

            var navigationPage = new NavigationPage(new MainPage());
            navigationPage.BarBackgroundColor = Color.FromHex("#00141C"); // Set your desired color here

            MainPage = navigationPage;
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

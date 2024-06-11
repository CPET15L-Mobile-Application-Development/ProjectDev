using projDevMain.Services;
using projDevMain.Views;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Gilroy-Bold.ttf", Alias = "Gilroy-Bold")]
[assembly: ExportFont("Gilroy-Heavy.ttf", Alias = "Gilroy-Heavy")]
[assembly: ExportFont("Gilroy-Light.ttf", Alias = "Gilroy-Light")]
[assembly: ExportFont("Gilroy-Medium.ttf", Alias = "Gilroy-Medium")]
[assembly: ExportFont("Gilroy-Regular.ttf", Alias = "Gilroy-Regular")]
namespace projDevMain
{
    public partial class App : Application
    {
        private static DatabaseService service;

        public static DatabaseService Service
        {
            get
            {
                if (service == null)
                {
                    service = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db3"));
                }
                return service;
            }
        }


        public App()
        {
            InitializeComponent();
            

            //SET THE SPLASHSCREEN AS THE INITIAL PAGE
            MainPage = new SplashPage();

            //CHANGE THE BAR BACKGROUND COLOR
            //NavigationPage navigationPage = new NavigationPage(new MainPage());
            //navigationPage.BarBackgroundColor = Xamarin.Forms.Color.FromHex("#00141C");


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

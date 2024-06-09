using projDevMain.Services;
using projDevMain.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


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

            // Set the SplashScreen as the initial page
            MainPage = new SplashPage();
           
         


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

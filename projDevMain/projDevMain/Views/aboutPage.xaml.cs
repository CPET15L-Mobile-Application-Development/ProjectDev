using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class aboutPage : ContentPage
    {
        public aboutPage()
        {
            InitializeComponent();

        }

        //OPEN THE GITHUB REPOSITORY OF THE PROJECT
        private async void githubRepo(object sender, EventArgs e)
        {
            // Check if a GitHub app is installed on the device
            if (await Launcher.CanOpenAsync("https://github.com"))
            {
                await Launcher.OpenAsync("https://github.com/CPET15L-Mobile-Application-Development/ProjectDev.git");
            }
            else
            {
                // Open the link in the user's preferred browser
                await Launcher.OpenAsync(new Uri("https://github.com/CPET15L-Mobile-Application-Development/ProjectDev.git"));
            }
        }
    }
}
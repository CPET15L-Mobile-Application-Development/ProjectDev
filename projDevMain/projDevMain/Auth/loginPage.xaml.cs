using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Services;
using projDevMain.ViewModels;
using projDevMain.Views;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loginPage : ContentPage
    {
        private DatabaseService _databaseService;

        public loginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            //NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void RegisterPage_Tapped(object sender, EventArgs e)
        {
            // Replace the current page with the registerPage
            Navigation.InsertPageBefore(new registerPage(), this);
            Navigation.PopAsync();
        }

        private async void signIn_Clicked(object sender, EventArgs e)
        {
            var usernameEntry = this.FindByName<Entry>("usernameEntry");
            var passwordEntry = this.FindByName<Entry>("passwordEntry");

            var user = _databaseService.GetUser(usernameEntry.Text, passwordEntry.Text);
            if (user != null)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to home page with username
                
               
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}

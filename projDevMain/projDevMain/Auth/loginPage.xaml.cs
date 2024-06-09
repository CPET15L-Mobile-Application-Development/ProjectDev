// loginpage.xaml.cs
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Services;
using System;

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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void RegisterPage_Tapped(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new registerPage(), this);
            Navigation.PopAsync();
        }
        //CHECKS THE DATABASE FOR CREDENTIALS
        private async void signIn_Clicked(object sender, EventArgs e)
        {
            var usernameEntry = this.FindByName<Entry>("usernameEntry");
            var passwordEntry = this.FindByName<Entry>("passwordEntry");

            var user = _databaseService.GetUser(usernameEntry.Text, passwordEntry.Text);
            if (user != null)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to home page with username
                Navigation.InsertPageBefore(new MainPage(user.Username), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}

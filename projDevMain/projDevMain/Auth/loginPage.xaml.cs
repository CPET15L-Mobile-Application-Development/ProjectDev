using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.Services;
using projDevMain.Models;
using System;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loginPage : ContentPage
    {
        private DatabaseService _databaseService;

        //INITIALIZE DATABASE FOR LOGIN FORM
        public loginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        //CHECKS IF THERE IS CURRENT ACCOUNT SESSION

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckForSavedSession();
        }
        //IF THERE IS CURRENT SESSION - NAVIGATES TO MAINPAGE
        private async void CheckForSavedSession()
        {
            if (Application.Current.Properties.ContainsKey("IsLoggedIn") && (bool)Application.Current.Properties["IsLoggedIn"])
            {
                if (Application.Current.Properties.ContainsKey("CurrentUserId"))
                {
                    int userId = (int)Application.Current.Properties["CurrentUserId"];
                    var user = _databaseService.GetUserById(userId);

                    if (user != null)
                    {
                        // Navigate to home page with user object
                        Navigation.InsertPageBefore(new MainPage(user), this);
                        await Navigation.PopAsync();
                    }
                }
            }
        }
        //GOTO REGISTER PAGE
        private void RegisterPage_Tapped(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new registerPage(), this);
            Navigation.PopAsync();
        }
        //CHECKS CREDENTIALS IN DATABASE
        private async void signIn_Clicked(object sender, EventArgs e)
        {
            var usernameEntry = this.FindByName<Entry>("usernameEntry");
            var passwordEntry = this.FindByName<Entry>("passwordEntry");

            var user = _databaseService.GetUser(usernameEntry.Text, passwordEntry.Text);
            if (user != null)
            {
                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["CurrentUserId"] = user.Id;
                await Application.Current.SavePropertiesAsync();

                await DisplayAlert("Success", "Login successful!", "OK");

                // Navigate to home page with user object
                Navigation.InsertPageBefore(new MainPage(user), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}

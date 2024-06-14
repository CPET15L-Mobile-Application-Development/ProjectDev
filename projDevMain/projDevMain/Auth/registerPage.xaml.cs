using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using projDevMain.ViewModels;
using projDevMain.Services;
using projDevMain.Models;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registerPage : ContentPage
    {
        private DatabaseService _databaseService; //SETTING UP THE DATABASE FOR REGISTER PAGE

        //INITIALIZE THE REGISTER PAGE
        public registerPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();


            //PASSWORD REQUIREMENTS MODAL
            DisplayAlert("Password Requirements",
                "Password must be:\n" +
                "- 8 to 15 characters long\n" +
                "- Contains at least one capital letter\n" +
                "- Contains at least one small letter\n" +
                "- Contains at least one number\n" +
                "- Contains at least one special character",
                "OK");

            name.Focus();

            

            //NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

       
        //GOTO LOGIN PAGE
        private void LoginPage_Tapped(object sender, EventArgs e)
        {
            // Replace the current page with the loginPage
            Navigation.InsertPageBefore(new loginPage(), this);
            Navigation.PopAsync();
        }
        //CHECKS THE PASSWORD VERIFICATIONS
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var bindingContext = (RegisterPageViewModel)BindingContext;

            if (entry.Placeholder == "Password")
            {
                bindingContext.Password = e.NewTextValue;
            }
            else if (entry.Placeholder == "Confirm Password")
            {
                bindingContext.ConfirmPassword = e.NewTextValue;
            }
        }
        //IF SUCCESSFUL SIGN UP - GOTO LOGIN PAGE
        private async void signUp_Clicked(object sender, EventArgs e)
        {
            var bindingContext = (RegisterPageViewModel)BindingContext;
            if (bindingContext.CanSignUp)
            {
                var user = new User
                {
                    Username = bindingContext.Username,
                    Password = bindingContext.Password
                };
                _databaseService.SaveUser(user);
                await DisplayAlert("Success", "Account registered successfully", "OK");

                // Navigate to login page
                Navigation.InsertPageBefore(new loginPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", "Please ensure all fields are filled correctly", "OK");
            }
        }

        //TERMS AND CONDITION FUNCTION

    }
}

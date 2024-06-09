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
        private DatabaseService _databaseService;
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
           

            //NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LoginPage_Tapped(object sender, EventArgs e)
        {
            // Replace the current page with the loginPage
            Navigation.InsertPageBefore(new loginPage(), this);
            Navigation.PopAsync();
        }

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

        //TERMS AND CONDITION FUNCTION
        private void termsCondition_modal_Tapped(object sender, EventArgs e)
        {

        }

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
                await DisplayAlert("Success","Account registered successfully","OK");
            
                //NAVIGATE TO HOMEPAGE
                Navigation.InsertPageBefore(new loginPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR","Please ensure all fields are filled","OK");
            }
        }
    }
}

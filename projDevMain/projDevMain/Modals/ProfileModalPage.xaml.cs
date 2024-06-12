using projDevMain.Models;
using projDevMain.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileModalPage : ContentPage
    {
        private User currentUser;       //INITIALIZE CURRENT USER
        private DatabaseService _databaseService;   //INITIALIZE DATABASE

        //INITIALIZE CURRENT USER INFOS
        public ProfileModalPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            _databaseService = new DatabaseService();
            UpdateUserDetails(user);
        }
        //UPDATES CURRENT USERS INFORMATION TO MODAL
        private void UpdateUserDetails(User user)
        {
            if ( !string.IsNullOrEmpty(user.ProfilePicture))
            {
               
                dp.Source = ImageSource.FromFile(user.ProfilePicture);
            }
            userfullName.Text = $"{user.FirstName} {user.MiddleName} {user.LastName}".Trim();
            userEmail.Text = user.Email;
            userContact.Text = user.ContactNumber;
        }
        //LOGGOUT FUNCTION AND DELETE THE CURRENT SESSION
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            // Clear user session or any related data
            Application.Current.Properties["IsLoggedIn"] = false;
            Application.Current.Properties["CurrentUserId"] = null;

            // Save properties if needed
            await Application.Current.SavePropertiesAsync();

            // Navigate to the login page
            Application.Current.MainPage = new NavigationPage(new loginPage());
        }
    }
}

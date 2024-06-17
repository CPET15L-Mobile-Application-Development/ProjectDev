using projDevMain.Models;
using projDevMain.Services;
using System;
using Xamarin.Essentials;
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
            userfullName.Text = $"{(string.IsNullOrEmpty(user.FirstName) ? "Edit Profile" : user.FirstName)} {(string.IsNullOrEmpty(user.LastName) ? "" : user.LastName)}".Trim();
            userEmail.Text = string.IsNullOrEmpty(user.Email) ? "Edit Profile" : user.Email;
            userContact.Text = string.IsNullOrEmpty(user.ContactNumber) ? "Edit Profile" : user.ContactNumber;
        }
        //LOGGOUT FUNCTION AND DELETE THE CURRENT SESSION
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            // Clear user session or any related data
            Application.Current.Properties["IsLoggedIn"] = false;
            Application.Current.Properties["CurrentUserId"] = null;

            // Save properties if needed
            await Application.Current.SavePropertiesAsync();

            var answer = await DisplayAlert("Logout", "Do you want to logout the current session?", "Yes", "No");
            if (answer)
            {
                // Navigate to the login page
                Application.Current.MainPage = new NavigationPage(new loginPage());
            }
            else
            {
                //CLOSE THE DISPLAY ALERT
            }
        }

        //GOTO FACEBOOK USING USER FACEBOOK
        private async void fb_Clicked(object sender, EventArgs e)
        {
            string facebookUrl = currentUser.Facebook;
            string facebookAppUriScheme = "fb://facewebmodal/f?href=" + Uri.EscapeDataString(facebookUrl);

            bool canOpenWithApp = await Launcher.TryOpenAsync(facebookAppUriScheme);

            
            if (string.IsNullOrEmpty(facebookUrl))
            {
                await DisplayAlert("ERROR", "Invalid URL!", "OK");
            } else 
            {
                await Browser.OpenAsync(facebookUrl);
            }
        }

        //GOTO INSTAGRAM USING USER INSTA
        private async void insta_Clicked(object sender, EventArgs e)
        {
            string url = currentUser.Instagram;

            if (string.IsNullOrEmpty(url))
            {
                await DisplayAlert("ERROR", "Invalid URL!", "OK");
            }
            else
            {
                await Browser.OpenAsync(url);
            }
        }
        //GOTO TWITTER USING USER TWITTER
        private async void x_Clicked(object sender, EventArgs e)
        {
            string url = currentUser.Twitter;

            if (string.IsNullOrEmpty(url))
            {
                await DisplayAlert("ERROR", "Invalid URL!", "OK");
            }
            else
            {
                await Browser.OpenAsync(url);
            }
        }
        //GOTO LINKEDIN USING USER LINKEDIN
        private async void link_Clicked(object sender, EventArgs e)
        {
            string url = currentUser.LinkedIn;

            if (string.IsNullOrEmpty(url))
            {
                await DisplayAlert("ERROR", "Invalid URL!", "OK");
            }
            else
            {
                await Browser.OpenAsync(url);
            }
        }
        //GOTO GITHUB USING USER GITHUB
        private async void git_Clicked(object sender, EventArgs e)
        {
            string url = currentUser.GitHub;

            if (string.IsNullOrEmpty(url))
            {
                await DisplayAlert("ERROR", "Invalid URL!", "OK");
            }
            else
            {
                await Browser.OpenAsync(url);
            }
        }
        //BACK MODAL FUNCTION WHEN CLOSE BUTTON CLICKED
        async void closeBTTN(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

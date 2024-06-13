using projDevMain.Modals;
using projDevMain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using projDevMain.Views;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class accountPage : ContentPage
    {
        private User currentUser;   //INITIALIZE CURRENT USER INFOS

        ObservableCollection<AccountImageModel> imageCollection;

       //INTIALIZE ACCOUNT PAGE
        public accountPage(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Display initial user details with placeholders
            UpdateUserDetails(currentUser);

            // Subscribe to the message
            MessagingCenter.Subscribe<AccountInfoModalPage, User>(this, "ProfileUpdated", (sender, updatedUser) =>
            {
                // Update the user details when the profile is updated
                currentUser = updatedUser;
                UpdateUserDetails(currentUser);
            });


            //STATIC DATA FOR THE IMAGE COLLECTION FOR THE ACCOUNT
            //PLEASE CHANGE THIS BASED ON THE DATABASE SQL FOR BETTER FUNCTIONALITY IF POSSIBLE
            imageCollection = new ObservableCollection<AccountImageModel>
            {
                new AccountImageModel {accImage = "noImageCard.png"},
                new AccountImageModel {accImage = "noImageCard.png"},
                new AccountImageModel {accImage = "noImageCard.png"},
                new AccountImageModel {accImage = "noImageCard.png"},
                new AccountImageModel {accImage = "noImageCard.png"},
                new AccountImageModel {accImage = "noImageCard.png"},
            };
            accImgDataView.ItemsSource = imageCollection;
        }
        //UPDATES USERS INFO UPON UPDATING IN MODAL
        private void UpdateUserDetails(User user)
        {
            // Update user details or set placeholders
            userFullName.Text = $"{(string.IsNullOrEmpty(user.FirstName) ? "Edit Profile" : user.FirstName)} {(string.IsNullOrEmpty(user.LastName) ? "" : user.LastName)}".Trim();
            userFirst.Text = string.IsNullOrEmpty(user.FirstName) ? "Edit Profile" : user.FirstName;
            userMiddle.Text = string.IsNullOrEmpty(user.MiddleName) ? "Edit Profile" : user.MiddleName;
            userLast.Text = string.IsNullOrEmpty(user.LastName) ? "Edit Profile" : user.LastName;
            userAge.Text = user.Age == 0 ? "Edit Profile" : user.Age.ToString();
            userEmail.Text = string.IsNullOrEmpty(user.Email) ? "Edit Profile" : user.Email;
            userContact.Text = string.IsNullOrEmpty(user.ContactNumber) ? "Edit Profile" : user.ContactNumber;
            userBio.Text = string.IsNullOrEmpty(user.BioCaption) ? "Edit Profile" : user.BioCaption;

            if (!string.IsNullOrEmpty(user.Wallpaper) && !string.IsNullOrEmpty(user.ProfilePicture))
            {
                wall.Source = ImageSource.FromFile(user.Wallpaper);
                dp.Source = ImageSource.FromFile(user.ProfilePicture);
            }  
        }



        //EDIT ACCOUNT INFORMATION MODAL FUNCTION
        private async void editInfo(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AccountInfoModalPage(currentUser));
        }

        //EDIT IMAGE INFORMATION MODAL FUNCTION
        private async void editImages(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ImageInfoModalPage());
        }

        //GOTO FACEBOOK USING USER FB
        private async void fb_Clicked(object sender, EventArgs e)
        {
            string facebookUrl = currentUser.Facebook;
            string facebookAppUriScheme = "fb://facewebmodal/f?href=" + Uri.EscapeDataString(facebookUrl);

            bool canOpenWithApp = await Launcher.TryOpenAsync(facebookAppUriScheme);

            if (!canOpenWithApp)
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
    }
}
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
        private User currentUser;

        public accountPage(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Display initial user details with placeholders
            UpdateUserDetails(currentUser);

            // Subscribe to the message for profile update
            MessagingCenter.Subscribe<AccountInfoModalPage, User>(this, "ProfileUpdated", (sender, updatedUser) =>
            {
                currentUser = updatedUser;
                UpdateUserDetails(currentUser);
            });

            // Subscribe to the message for image update
            MessagingCenter.Subscribe<ImageInfoModalPage, User>(this, "ImagesUpdated", (sender, updatedUser) =>
            {
                currentUser = updatedUser;
                UpdateUserImages(currentUser);
            });

            // Initial call to set images
            UpdateUserImages(currentUser);
        }

        private void UpdateUserDetails(User user)
        {
            userFullName.Text = $"{(string.IsNullOrEmpty(user.FirstName) ? "Edit Profile" : user.FirstName)} {(string.IsNullOrEmpty(user.LastName) ? "" : user.LastName)}".Trim();
            userFirst.Text = string.IsNullOrEmpty(user.FirstName) ? "Edit Profile" : user.FirstName;
            userMiddle.Text = string.IsNullOrEmpty(user.MiddleName) ? "Edit Profile" : user.MiddleName;
            userLast.Text = string.IsNullOrEmpty(user.LastName) ? "Edit Profile" : user.LastName;
            userAge.Text = user.Age == 0 ? "Edit Profile" : user.Age.ToString();
            userEmail.Text = string.IsNullOrEmpty(user.Email) ? "Edit Profile" : user.Email;
            userContact.Text = string.IsNullOrEmpty(user.ContactNumber) ? "Edit Profile" : user.ContactNumber;
            userBio.Text = string.IsNullOrEmpty(user.BioCaption) ? "Edit Profile" : user.BioCaption;
            dp.Source = string.IsNullOrEmpty(user.ProfilePicture) ? "noImageProf.png" : user.ProfilePicture;
            wall.Source = string.IsNullOrEmpty(user.Wallpaper) ? "noImageWall.png" : user.Wallpaper;

            // Notify homePage about the profile update
            MessagingCenter.Send(this, "ProfileUpdated", user);
        }

        private void UpdateUserImages(User user)
        {
            image1.Source = string.IsNullOrEmpty(user.Image1) ? "noImageCard.png" : user.Image1;
            image2.Source = string.IsNullOrEmpty(user.Image2) ? "noImageCard.png" : user.Image2;
            image3.Source = string.IsNullOrEmpty(user.Image3) ? "noImageCard.png" : user.Image3;
            image4.Source = string.IsNullOrEmpty(user.Image4) ? "noImageCard.png" : user.Image4;
            image5.Source = string.IsNullOrEmpty(user.Image5) ? "noImageCard.png" : user.Image5;
            image6.Source = string.IsNullOrEmpty(user.Image6) ? "noImageCard.png" : user.Image6;
        }



        //EDIT ACCOUNT INFORMATION MODAL FUNCTION
        private async void editInfo(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AccountInfoModalPage(currentUser));
        }

        //EDIT IMAGE INFORMATION MODAL FUNCTION
        private async void editImages(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ImageInfoModalPage(currentUser));
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

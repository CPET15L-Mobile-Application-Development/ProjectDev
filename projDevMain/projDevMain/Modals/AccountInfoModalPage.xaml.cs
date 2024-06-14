using projDevMain.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountInfoModalPage : ContentPage
    {
        private User currentUser; // Saves the info for current user
        string pathFilewall;        // File path of wallpaper
        string pathFiledp;          // File path of profile picture

        // Initialize account info modal page informations
        public AccountInfoModalPage(User user)
        {
            InitializeComponent();

            currentUser = user;

            firstName_Entry.Text = currentUser.FirstName;
            middleName_Entry.Text = currentUser.MiddleName;
            lastName_Entry.Text = currentUser.LastName;
            age_Entry.Text = currentUser.Age.ToString();
            email_Entry.Text = currentUser.Email;
            num_Entry.Text = currentUser.ContactNumber;

            dp.Source = string.IsNullOrEmpty(currentUser.ProfilePicture) ? "noImageProf.png" : currentUser.ProfilePicture;
            wall.Source = string.IsNullOrEmpty(currentUser.Wallpaper) ? "noImageWall.png" : currentUser.Wallpaper;

            pathFilewall = currentUser.Wallpaper;
            pathFiledp = currentUser.ProfilePicture;
            bio_Entry.Text = currentUser.BioCaption;

            facebook_Entry.Text = currentUser.Facebook;
            insta_Entry.Text = currentUser.Instagram;
            x_Entry.Text = currentUser.Twitter;
            lI_Entry.Text = currentUser.LinkedIn;
            github_Entry.Text = currentUser.GitHub;
        }

        // Saves the changed informations to current user informations
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Update user details
            currentUser.FirstName = firstName_Entry.Text;
            currentUser.MiddleName = middleName_Entry.Text;
            currentUser.LastName = lastName_Entry.Text;
            currentUser.BioCaption = bio_Entry.Text;

            if (int.TryParse(age_Entry.Text, out int age))
            {
                currentUser.Age = age;
            }
            else
            {
                await DisplayAlert("Error", "Invalid age entered", "OK");
                return;
            }

            currentUser.Email = email_Entry.Text;
            currentUser.ContactNumber = num_Entry.Text;

            // Only update the wallpaper and profile picture if new paths are set
            if (!string.IsNullOrEmpty(pathFilewall))
            {
                currentUser.Wallpaper = pathFilewall;
            }

            if (!string.IsNullOrEmpty(pathFiledp))
            {
                currentUser.ProfilePicture = pathFiledp;
            }

            currentUser.Facebook = facebook_Entry.Text;
            currentUser.Instagram = insta_Entry.Text;
            currentUser.Twitter = x_Entry.Text;
            currentUser.LinkedIn = lI_Entry.Text;
            currentUser.GitHub = github_Entry.Text;

            // Update user in the database
            App.Service.UpdateUser(currentUser);

            // Send message to notify accountPage of profile update
            MessagingCenter.Send(this, "ProfileUpdated", currentUser);

            // Navigate back to accountPage
            await Navigation.PopModalAsync();
        }

        // Image import for wallpaper
        private async void wall_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select an image file"
                });
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var filePath = await SaveFileToLocalStorage(result.FileName, stream);
                    pathFilewall = filePath;
                    wall.Source = ImageSource.FromFile(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }

        // Image import for profile picture
        private async void dp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select an image file"
                });
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var filePath = await SaveFileToLocalStorage(result.FileName, stream);
                    pathFiledp = filePath;
                    dp.Source = ImageSource.FromFile(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }

        // Saves the images to local storage
        private async Task<string> SaveFileToLocalStorage(string fileName, Stream fileStream)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }
    }
}

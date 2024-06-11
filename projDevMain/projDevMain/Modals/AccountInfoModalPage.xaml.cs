using System;
using System.Text;
using System.Threading.Tasks;
using projDevMain.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace projDevMain.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountInfoModalPage : ContentPage
    {
        private User currentUser;
        public AccountInfoModalPage(User user)
        {
            InitializeComponent();
            currentUser = user;

            firstName_Entry.Text = currentUser.FirstName;
            middleName_Entry.Text = currentUser.MiddleName;
            lastName_Entry.Text = currentUser.LastName;
            age_Entry.Text += currentUser.Age;
            email_Entry.Text = currentUser.Email;
            num_Entry.Text = currentUser.ContactNumber;

            facebook_Entry.Text = currentUser.Facebook;
            insta_Entry.Text = currentUser.Instagram;
            x_Entry.Text = currentUser.Twitter;
            lI_Entry.Text = currentUser.LinkedIn;
            github_Entry.Text = currentUser.GitHub;
        }

        private async void OnSaveClicked(object sender, System.EventArgs e)
        {
            // Update user details
            currentUser.FirstName = firstName_Entry.Text;
            currentUser.MiddleName = middleName_Entry.Text;
            currentUser.LastName = lastName_Entry.Text;
            currentUser.Age = int.Parse(age_Entry.Text);
            currentUser.Email = email_Entry.Text;
            currentUser.ContactNumber = num_Entry.Text;
            currentUser.Wallpaper = wallEntry.Text;
            currentUser.ProfilePicture = dpEntry.Text;

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
            await Navigation.PopAsync();
        }

        private async void wall_Clicked(object sender, System.EventArgs e)
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
                    wallEntry.Text = filePath;
                    wallpaper.Source = ImageSource.FromFile(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }

        //SAVE IMAGE LOCALLY FOR FASTER RETRVIEVE
        private async Task<string> SaveFileToLocalStorage(string fileName, Stream fileStream)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }

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

                    dpEntry.Text = filePath;
                    dp.Source = ImageSource.FromFile(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }
    }
}

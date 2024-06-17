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
    public partial class ImageInfoModalPage : ContentPage
    {
        private User currentUser;
        private string Image1, Image2, Image3, Image4, Image5, Image6;

        public ImageInfoModalPage(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;

            image1.Source = string.IsNullOrEmpty(currentUser.Image1) ? "noImageCard.png" : currentUser.Image1;
            image2.Source = string.IsNullOrEmpty(currentUser.Image2) ? "noImageCard.png" : currentUser.Image2;
            image3.Source = string.IsNullOrEmpty(currentUser.Image3) ? "noImageCard.png" : currentUser.Image3;
            image4.Source = string.IsNullOrEmpty(currentUser.Image4) ? "noImageCard.png" : currentUser.Image4;
            image5.Source = string.IsNullOrEmpty(currentUser.Image5) ? "noImageCard.png" : currentUser.Image5;
            image6.Source = string.IsNullOrEmpty(currentUser.Image6) ? "noImageCard.png" : currentUser.Image6;

            Image1 = currentUser.Image1;
            Image2 = currentUser.Image2;
            Image3 = currentUser.Image3;
            Image4 = currentUser.Image4;
            Image5 = currentUser.Image5;
            Image6 = currentUser.Image6;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Only update the images if new paths are set
            currentUser.Image1 = Image1;
            currentUser.Image2 = Image2;
            currentUser.Image3 = Image3;
            currentUser.Image4 = Image4;
            currentUser.Image5 = Image5;
            currentUser.Image6 = Image6;

            // Update user in the database
            App.Service.UpdateUser(currentUser);

            // Send message to notify accountPage of profile update
            MessagingCenter.Send(this, "ImagesUpdated", currentUser);

            // Navigate back to accountPage
            await Navigation.PopModalAsync();
        }

        private async void image1_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image1 = result;
                image1.Source = ImageSource.FromFile(result);
            });
        }

        private async void image2_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image2 = result;
                image2.Source = ImageSource.FromFile(result);
            });
        }

        private async void image3_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image3 = result;
                image3.Source = ImageSource.FromFile(result);
            });
        }

        //BACK MODAL FUNCTION WHEN CLOSE BUTTON CLICKED
        async void closeBTTN(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        private async void image4_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image4 = result;
                image4.Source = ImageSource.FromFile(result);
            });
        }

        private async void image5_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image5 = result;
                image5.Source = ImageSource.FromFile(result);
            });
        }

        private async void image6_Clicked(object sender, EventArgs e)
        {
            await PickImage(result => {
                Image6 = result;
                image6.Source = ImageSource.FromFile(result);
            });
        }

        private async Task PickImage(Action<string> callback)
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
                    callback(filePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import image: {ex.Message}", "OK");
            }
        }

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

using projDevMain.Modals;
using projDevMain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class accountPage : ContentPage
    {
        ObservableCollection<AccountImageModel> imageCollection;
        public accountPage()
        {
            InitializeComponent();

            //STATIC DATA FOR THE IMAGE COLLECTION FOR THE ACCOUNT
            //PLEASE CHANGE THIS BASED ON THE DATABASE SQL FOR BETTER FUNCTIONALITY IF POSSIBLE
            imageCollection = new ObservableCollection<AccountImageModel>
            {
                new AccountImageModel {accImage = "JBimage1.png"},
                new AccountImageModel {accImage = "JBimage2.png"},
                new AccountImageModel {accImage = "JBimage3.png"},
                new AccountImageModel {accImage = "JBimage4.png"},
                new AccountImageModel {accImage = "JBimage5.png"},
                new AccountImageModel {accImage = "JBimage6.png"},
            };
            accImgDataView.ItemsSource = imageCollection;
        }

        //EDIT ACCOUNT INFORMATION MODAL FUNCTION
        private async void editInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountInfoModalPage());
        }

        //EDIT IMAGE INFORMATION MODAL FUNCTION
        private async void editImages(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ImageInfoModalPage());
        }
    }
}
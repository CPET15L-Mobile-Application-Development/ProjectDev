using projDevMain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageInfoModalPage : ContentPage
	{
		ObservableCollection<AccountImageModel> imageCollection;
        public ImageInfoModalPage()
        {
            InitializeComponent();
            //STATIC DATA FOR THE IMAGE COLLECTION FOR THE ACCOUNT
            //PLEASE CHANGE THIS BASED ON THE DATABASE SQL FOR BETTER FUNCTIONALITY IF POSSIBLE
           //imageCollection = new ObservableCollection<AccountImageModel>
           // {
           //     new AccountImageModel {imgTitle = "Image 1"},
           //     new AccountImageModel { imgTitle = "Image 2"},
           //     new AccountImageModel { imgTitle = "Image 3"},
           //     new AccountImageModel {imgTitle = "Image 4"},
           //     new AccountImageModel {imgTitle = "Image 5"},
           //     new AccountImageModel {imgTitle = "Image 6"},         
           // };
           // accImgDataView.ItemsSource = imageCollection; 
        }
	}
}
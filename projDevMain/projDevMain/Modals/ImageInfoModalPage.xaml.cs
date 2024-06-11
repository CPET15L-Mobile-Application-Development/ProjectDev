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
            imageCollection = new ObservableCollection<AccountImageModel>
            {
                new AccountImageModel {accImage = "JBimage1.png", imgTitle = "Image 1"},
                new AccountImageModel {accImage = "JBimage2.png", imgTitle = "Image 2"},
                new AccountImageModel {accImage = "JBimage3.png", imgTitle = "Image 3"},
                new AccountImageModel {accImage = "JBimage4.png", imgTitle = "Image 4"},
                new AccountImageModel {accImage = "JBimage5.png", imgTitle = "Image 5"},
                new AccountImageModel {accImage = "JBimage6.png", imgTitle = "Image 6"},         
            };
            accImgDataView.ItemsSource = imageCollection;
        }
	}
}
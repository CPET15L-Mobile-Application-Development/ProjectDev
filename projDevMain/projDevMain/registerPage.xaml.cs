using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class registerPage : ContentPage
	{
		public registerPage ()
		{
			InitializeComponent ();

            //NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LoginPage_Tapped(object sender, EventArgs e)
        {
			Navigation.PushAsync(new loginPage());
        }
    }
}
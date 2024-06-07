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
	public partial class loginPage : ContentPage
	{
		public loginPage ()
		{
			InitializeComponent ();

			//NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void RegisterPage_Tapped(object sender, EventArgs e)
        {
            // Replace the current page with the loginPage
            Navigation.InsertPageBefore(new registerPage(), this);
            Navigation.PopAsync();
        }
    }
}
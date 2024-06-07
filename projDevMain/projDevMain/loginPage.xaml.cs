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

        }

        //SIGN-IN BUTTON
        private void signIn_Clicked(object sender, EventArgs e)
        {

        }

        //REGISTER LINK
        private void registerPage_Tapped(object sender, EventArgs e)
        {

        }

    }
}
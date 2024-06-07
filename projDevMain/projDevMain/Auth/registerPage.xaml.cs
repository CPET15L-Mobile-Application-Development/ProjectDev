using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registerPage : ContentPage
    {
        public registerPage()
        {
            InitializeComponent();

            //NAVIGATION PAGE PROPERTIES
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LoginPage_Tapped(object sender, EventArgs e)
        {
            // Replace the current page with the loginPage
            Navigation.InsertPageBefore(new loginPage(), this);
            Navigation.PopAsync();
            
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((RegisterPageViewModel)BindingContext).Password = e.NewTextValue; //pass the value from xaml to model for verification
        }
    }
}

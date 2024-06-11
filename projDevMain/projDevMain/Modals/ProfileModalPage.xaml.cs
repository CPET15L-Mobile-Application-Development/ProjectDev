using projDevMain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projDevMain.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileModalPage : ContentPage
    {
        public ProfileModalPage(User user)
        {
            InitializeComponent();
            UpdateUserDetails(user);
        }

        private void UpdateUserDetails(User user)
        {
            dp.Source = user.ProfilePicture;
            userfullName.Text = $"{user.FirstName} {user.MiddleName} {user.LastName}".Trim();
            userEmail.Text = user.Email;
            userContact.Text = user.ContactNumber;
        }

        private void OnLogout_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new loginPage());
        }
    }
}

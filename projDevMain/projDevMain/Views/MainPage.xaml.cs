using projDevMain.Models;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace projDevMain
{
    public partial class MainPage : TabbedPage
    {
        
        private User currentUser;       //INITIALIZE CURRENT USER

        //INITIALIZE CURRENT USER INFORMATION AND PASSES TO DIREFFERENT PAGES
        public MainPage(User user)
        {
            InitializeComponent();
            currentUser = user;

            var homePage = new homePage(currentUser);
            var listPage = new listPage();
            var accountPage = new accountPage(currentUser);
            var aboutPage = new aboutPage();

            homePage.SetUsername(currentUser.Username);

            Children.Add(new homePage(currentUser)
            {
                Title = "Homes",
                IconImageSource = "homeIcon.png"
            });
            Children.Add(new listPage()
            {
                Title = "List",
                IconImageSource = "listIcon.png"
            });
            Children.Add(new accountPage(currentUser)
            {
                Title = "Account",
                IconImageSource = "accountIcon.png"
            });
            Children.Add(new aboutPage()
            {
                Title = "About",
                IconImageSource = "aboutIcon.png"
            });
        }



    }
}
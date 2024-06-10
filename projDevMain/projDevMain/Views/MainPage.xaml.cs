using Xamarin.Forms;

namespace projDevMain
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //INSTEAD USING XAML FOR TABBED PAGE, USED C# FOR EASY ACCESS TO BACKEND
        public MainPage(string username) : this()
        {
            var homePage = new homePage(username);
            homePage.SetUsername(username);
            Children.Add( new homePage(username)
            {
                Title = "Home",
                IconImageSource = "homeIcon.png"
            });
            // Add other pages
            Children.Add(new listPage()
            {
                Title = "List",
                IconImageSource = "listIcon.png"
            });
            Children.Add(new accountPage()
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
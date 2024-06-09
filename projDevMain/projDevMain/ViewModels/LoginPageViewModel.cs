// ViewModels/LoginPageViewModel.cs
using System.ComponentModel;
using System.Windows.Input;
using projDevMain.Models;
using projDevMain.Services;
using Xamarin.Forms;

namespace projDevMain.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private DatabaseService _databaseService;

        public LoginPageViewModel()
        {
            _databaseService = new DatabaseService();
            LoginCommand = new Command(OnLogin);
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        private void OnLogin()
        {
            var user = _databaseService.GetUser(Username, Password);
            if (user != null)
            {
                // Navigate to account page
            }
            else
            {
                // Display login error
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

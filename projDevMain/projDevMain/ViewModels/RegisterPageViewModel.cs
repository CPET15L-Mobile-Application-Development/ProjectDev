using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Input;
using projDevMain.Models;
using projDevMain.Services;
using Xamarin.Forms;

namespace projDevMain.ViewModels
{
    public class RegisterPageViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private DatabaseService _databaseService;

        public RegisterPageViewModel()
        {
            _databaseService = new DatabaseService();
            RegisterCommand = new Command(OnRegister);
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSignUp)); // Notify that CanSignUp might need to be recalculated
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanSignUp)); // Notify that CanSignUp might need to be recalculated
            }
        }

        public bool CanSignUp
        {
            get
            {
                //CHECKS IF THE ENTRY FIELD HAS STRING
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
                    return false;
                //CHECKS PASSWORD REQUIREMENTS
                var hasNumber = Password.Any(char.IsDigit);
                var hasUpperChar = Password.Any(char.IsUpper);
                var hasLowerChar = Password.Any(char.IsLower);
                var hasSpecialChar = Password.Any(c => !char.IsLetterOrDigit(c));
                var isLengthValid = Password.Length >= 8 && Password.Length <= 15;

                //CHECKS PASSWORD IS SAME TO CONFIRM PASSWORD, WITH PASSWORD REQUIREMENTS
                return Password == ConfirmPassword && hasNumber && hasUpperChar && hasLowerChar && hasSpecialChar && isLengthValid;
            }
        }

        public ICommand RegisterCommand { get; }

        private void OnRegister()
        {
            if (CanSignUp)
            {
                var user = new User
                {
                    Username = Username,
                    Password = Password
                };
                _databaseService.SaveUser(user);
                // Navigate to login or account page
            }
            else
            {
                // Display password mismatch or validation error
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

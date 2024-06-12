using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
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
        private string _passwordValidationMessage;
        private string _confirmPasswordValidationMessage;
        private bool _isTermsAccepted;
        private DatabaseService _databaseService;

        //

        public RegisterPageViewModel()
        {
            _databaseService = new DatabaseService();
            RegisterCommand = new Command(OnRegister);
        }
        //GET USERNAME -> DATABASE
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
        //GET USER PASS -> DATABASE
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ValidatePassword();
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }
        //GET USER CONFIRM PASS
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                ValidateConfirmPassword();
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }
        //CHECKS FOR TERMS CHECKBOX
        public bool IsTermsAccepted
        {
            get { return _isTermsAccepted; }
            set
            {
                _isTermsAccepted = value;
                OnPropertyChanged(nameof(IsTermsAccepted));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }
        //THROWS PASSWORD VALIDATION MESSAGES
        public string PasswordValidationMessage
        {
            get { return _passwordValidationMessage; }
            private set
            {
                _passwordValidationMessage = value;
                OnPropertyChanged(nameof(PasswordValidationMessage));
            }
        }
        //THROWS MESSAGE IF PASS == CONFIRM PASS
        public string ConfirmPasswordValidationMessage
        {
            get { return _confirmPasswordValidationMessage; }
            private set
            {
                _confirmPasswordValidationMessage = value;
                OnPropertyChanged(nameof(ConfirmPasswordValidationMessage));
            }
        }
        //CHECKS THE PASSWORD REQS AND THROWS BOOL TO THE BUTTON
        public bool CanSignUp
        {
            get
            {
                return string.IsNullOrEmpty(PasswordValidationMessage)
                       && string.IsNullOrEmpty(ConfirmPasswordValidationMessage)
                       && !string.IsNullOrEmpty(Username)
                       && !string.IsNullOrEmpty(Password)
                       && !string.IsNullOrEmpty(ConfirmPassword)
                       && IsTermsAccepted;
            }
        }

        public ICommand RegisterCommand { get; }

        //SAVES CREDENTIALS TO DATABASE AND ADD USER TO DATABASE

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
        // PASSWORD VALIDATION MESSAGES
        private void ValidatePassword()
        {
            var hasNumber = Password.Any(char.IsDigit);
            var hasUpperChar = Password.Any(char.IsUpper);
            var hasLowerChar = Password.Any(char.IsLower);
            var hasSpecialChar = Password.Any(c => !char.IsLetterOrDigit(c));
            var isLengthValid = Password.Length >= 8 && Password.Length <= 15;

            if (!isLengthValid)
            {
                PasswordValidationMessage = "Password must be 8 to 15 characters long.";
            }
            else if (!hasUpperChar)
            {
                PasswordValidationMessage = "Password must contain at least one uppercase letter.";
            }
            else if (!hasLowerChar)
            {
                PasswordValidationMessage = "Password must contain at least one lowercase letter.";
            }
            else if (!hasNumber)
            {
                PasswordValidationMessage = "Password must contain at least one number.";
            }
            else if (!hasSpecialChar)
            {
                PasswordValidationMessage = "Password must contain at least one special character.";
            }
            else
            {
                PasswordValidationMessage = string.Empty;
            }
        }
        //CONFIRM PASSWORD VALIDATION MESSAGE
        private void ValidateConfirmPassword()
        {
            if (Password != ConfirmPassword)
            {
                ConfirmPasswordValidationMessage = "Passwords do not match.";
            }
            else
            {
                ConfirmPasswordValidationMessage = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace projDevMain
{
    public class RegisterPageViewModel : INotifyPropertyChanged
    {
        private string _password;
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
        //password verification
        public bool CanSignUp
        {
            get
            {
                if (string.IsNullOrEmpty(Password))
                    return false;

                var hasNumber = Password.Any(char.IsDigit);
                var hasUpperChar = Password.Any(char.IsUpper);
                var hasLowerChar = Password.Any(char.IsLower);
                var hasSpecialChar = Password.Any(c => !char.IsLetterOrDigit(c));

                return hasNumber && hasUpperChar && hasLowerChar && hasSpecialChar;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

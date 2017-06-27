using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using TicketReader.Models;
using TicketReader.Services;

namespace TicketReader.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private DialogService dialogService;
        private NavigationService navigationSevice;
        private ApiService apiService;
        private bool isEnabled;
        private bool isRunning;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string UserId { get; set; }
        public string Password { get; set; }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
        }

        #endregion

        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            navigationSevice = new NavigationService();
            apiService = new ApiService();
            UserId = "Pandian.eee@gmail.com";
            Password = "Tickets123";
            IsEnabled = false;
        } 
        #endregion

        #region Commands
        public ICommand LoginUserCommand
        {
            get { return new RelayCommand(LoginUser); }
        }

        private async void LoginUser()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                await dialogService.ShowMessage("Error", "You must enter a user name.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a password.");
                return;
            }

            IsEnabled = false;
            IsRunning = true;

            // Invoke Backend to validate user
            var loginRequest = new LoginRequest
            {
                Email = UserId,
                Password = Password
            };
            var response = await apiService.LoginUser(
                "http://checkticketsback.azurewebsites.net/",
                "/api",
                "/Users/Login",
                loginRequest);
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", "User Id or Password is incorrect!");
            }

            IsEnabled = true;
            IsRunning = false;

            // Invoke the Singleton Method of "MainViewModel" and Store the "UserInfo"
            //  returned by Backend upon successful login.
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.UserDetails = (UserInfo)response.Result;

            // Instantiate the CheckTicketPage ViewModel and 
            //  invoke the Navigation Service in order to Navigate to the Ticket Page.
            mainViewModel.CheckTicket = new CheckTicketViewModel();
            await navigationSevice.Navigate("CheckTicketPage");

        }
        #endregion
    }
}

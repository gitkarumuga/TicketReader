using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketReader.Models;
using TicketReader.Services;
using Xamarin.Forms;

namespace TicketReader.ViewModels
{
    public class CheckTicketViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private DialogService dialogService;
        private ApiService apiService;
        private NewTicketInfo newTicketInfo;
        private Color ticketStatusColor;
        private string ticketStatus;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Properties
        public string TicketCode { get; set; }
        public string TicketStatus
        {
            get
            {
                return ticketStatus;
            }
            set
            {
                if (ticketStatus != value)
                {
                    ticketStatus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TicketStatus"));
                }
            }
        }

        public Color TicketStatusColor
        {
            get
            {
                return ticketStatusColor;
            }
            set
            {
                if (ticketStatusColor != value)
                {
                    ticketStatusColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TicketStatusColor"));
                }   
            }
        }

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

        public CheckTicketViewModel()
        {
            TicketStatus = "Wait to read a ticket...";
            TicketStatusColor = Color.Black;
            dialogService = new DialogService();
            apiService = new ApiService();
            IsEnabled = false;
        }

        #region Commands
        public ICommand CheckTicketStatusCommand
        {
            get { return new RelayCommand(CheckTicketStatus); }
        }

        private async void CheckTicketStatus()
        {
            if (!(TicketCode.Length == 4))
            {
                await dialogService.ShowMessage("Error", "Ticket Code must be 4 characters.");
                return;
            }

            // Invoke Backend to check if a Ticket already exists
            var responseGetTicket = await apiService.GetTicket<TicketDetails>(
                "http://checkticketsback.azurewebsites.net/",
                "/api",
                "/Tickets",
                TicketCode);
            if (responseGetTicket.IsSuccess)
            {
                TicketStatus = string.Format("{0}, {1}", TicketCode, "TICKET READ BEFORE!");
                TicketStatusColor = Color.IndianRed;
                return;
            }

            // Invoke the Backend to Save a new Ticket
            newTicketInfo = new NewTicketInfo
            {
                TicketCode = TicketCode,
                DateTime = DateTime.Today,
                UserId = MainViewModel.GetInstance().UserDetails.UserId
            };
            var responseSaveTicket = await apiService.SaveTicket<NewTicketInfo>(
                "http://checkticketsback.azurewebsites.net/",
                "/api",
                "/Tickets",
                newTicketInfo);
            if (responseSaveTicket.IsSuccess)
            {
                TicketStatus = string.Format("{0}, {1}", TicketCode, "ALLOW ACCESS!");
                TicketStatusColor = Color.Green;
                return;
            }

            // Error message if could not save a ticket
            TicketStatus = string.Format("{0}, {1}", TicketCode, "ERROR IN CHECKING TICKET!");
            TicketStatusColor = Color.YellowGreen;

        }
        #endregion
    }
}

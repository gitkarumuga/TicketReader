using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReader.Models;

namespace TicketReader.ViewModels
{
    public class MainViewModel
    {

        #region Properties
        public LoginViewModel Login { get; set; }
        public CheckTicketViewModel CheckTicket { get; set; }
        public UserInfo UserDetails { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            Login = new LoginViewModel();
            instance = this;
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        } 
        #endregion

    }
}

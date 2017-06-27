using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReader.Services
{
    public class DialogService
    {

        #region Methods
        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
            return;
        }

        public async Task ShowConfirm(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
            return;
        } 
        #endregion

    }
}

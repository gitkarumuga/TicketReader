using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReader.Views;

namespace TicketReader.Services
{
    public class NavigationService
    {

        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CheckTicketPage":
                    await App.Current.MainPage.Navigation.PushAsync(new CheckTicketPage());
                    break;

                default:
                    break;
            }
        }

    }
}

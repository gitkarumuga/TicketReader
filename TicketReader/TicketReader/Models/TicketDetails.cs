using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReader.Models
{
    public class TicketDetails
    {

        #region Properties
        public string TicketId { get; set; }
        public string TicketCode { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; } 
        #endregion

    }
}

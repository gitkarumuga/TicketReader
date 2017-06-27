using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReader.Models
{
    public class NewTicketInfo
    {

        public string TicketCode { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
    }
}

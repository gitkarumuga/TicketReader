using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReader.Models
{
    public class LoginRequest
    {

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; } 
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    public class LoginD
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int clientId { get; set; }

        public string RequestIp { get; set; }
        public string ErrorMsg { get; set; }

    }
}

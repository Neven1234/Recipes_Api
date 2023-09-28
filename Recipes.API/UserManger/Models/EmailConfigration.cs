using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManger.Models
{
    public class EmailConfigration
    {
        public string From { get; init; }
        public string SmtpServer { get; init; }
        public int Port { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}

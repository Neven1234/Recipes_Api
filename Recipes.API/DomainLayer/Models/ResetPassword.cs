using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ResetPassword
    {
        public string Passwored { get; set; }
        public string ConfirmPasswored { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ChangePassword
    {
        public string OldPasswored { get; set; }

        public string NewPasswored { get; set; }
        public string ConfirmPasswored { get; set; }
    }
}

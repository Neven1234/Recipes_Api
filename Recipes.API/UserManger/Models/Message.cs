using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UserManger.Models
{
    public class Message
    {
        private string[] strings;
        private string v;
        private Task<string> confirmationLinke;

        public List<MailboxAddress> To { get; set; }
        
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
        }

        public Message(string[] strings, string v, Task<string> confirmationLinke)
        {
            this.strings = strings;
            this.v = v;
            this.confirmationLinke = confirmationLinke;
        }
    }
}

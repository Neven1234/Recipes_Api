using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManger.Models;

namespace UserManger.Service
{
    public class EmailService : IEmail
    {

        private readonly EmailConfigration _emailConfigration;

        public EmailService(EmailConfigration emailConfigration)
        {
            this._emailConfigration = emailConfigration;
        }
        public void SendEmail(Message messages)
        {
            var emailMessage = CreateEmailMessage(messages);
            Send(emailMessage);

        }
        private MimeMessage CreateEmailMessage(Message messages)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", this._emailConfigration.From));
            emailMessage.To.AddRange(messages.To);
            emailMessage.Subject = messages.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = messages.Content
            };
            return emailMessage;
        }
        private string Send(MimeMessage mailMessage)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(this._emailConfigration.SmtpServer, this._emailConfigration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(this._emailConfigration.UserName, this._emailConfigration.Password);

                client.Send(mailMessage);
                return "success";
            }
            catch (Exception ex)
            {
                //log an error message or throw an exception or both.
                return ex.Message;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}

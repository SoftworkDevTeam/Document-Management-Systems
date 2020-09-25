using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Helper
{
    public class Mailer
    {
        private readonly AppDbContext dbContext;

        public Mailer(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void UserRegistration(IConfiguration configuration, string name, string email, string password, string link)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Telnet Nigeria Limited", configuration["Mailing:MailFrom"]);
                message.From.Add(from);
                
                MailboxAddress to = new MailboxAddress(name,
                email);
                message.To.Add(to);

                message.Subject = "EDMS Registration";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>EDMS Email Confirmation</h1><br/>" +
                    "<h3>Click <a href=" + link + ">here</a> to confirm your email</h3><br/>" +
                    "Your Password is  "+password;
                bodyBuilder.TextBody = "Click this link " + link + " to confirm your email";

                //bodyBuilder.Attachments.Add(path);
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                //client.SslProtocols |= SslProtocols.Ssl2;
                
                client.Connect(configuration["Mailing:SmtpHost"], Convert.ToInt32(configuration["Mailing:SmtpPort"]), true);
                client.Authenticate(configuration["Mailing:Username"].ToString(), configuration["Mailing:Password"].ToString());

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();
            }
        }
    }
}

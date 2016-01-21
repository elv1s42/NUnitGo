using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using NunitGo.Utils;

namespace NunitGo.NunitGoItems.Subscriptions
{
    internal static class EmailHelper
    {
        public static bool SingleSend(Address from, Address to, string mailSubject, string mailBody, bool isBodyHtml = true)
        {
            try
            {
                var fromAddress = new MailAddress(from.Email, from.Name);
                var toAddress = new MailAddress(to.Email, to.Name);
                var smtp = new SmtpClient
                {
                    Host = NunitGoHelper.Configuration.SmtpHost,
                    Port = NunitGoHelper.Configuration.SmtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Email.Split('@').First(), from.Password),
                    Timeout = 10000
                };
                Log.Write(String.Format("Sending email from {0}, {1}, {2}, to {3}, {4}. " + Environment.NewLine +
                                        "Host: {5}, Port: {6}. " + from.Email.Split('@').First(), 
                                        from.Email, from.Name, from.Password, to.Email, to.Name, smtp.Host, smtp.Port));
                using (smtp)
                {
                    using (var message = new MailMessage(fromAddress, toAddress){IsBodyHtml = isBodyHtml})
                    {
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Failure in SingleSend method!");
                return false;
            }
            return true;
        }

        public static void Send(List<Address> mailFromList, List<Address> targetEmails,
            string mailSubject, string mailBody, bool isBodyHtml = true)
        {
            foreach (var address in targetEmails)
            {
                var fromMails = mailFromList;
                var success = false;
                while (!success && fromMails.Any())
                {
                    success = SingleSend(fromMails.First(), address, mailSubject, mailBody, isBodyHtml);
                    if (!success)
                        fromMails = fromMails.Skip(1).ToList();
                }
            }
        }
    }
}

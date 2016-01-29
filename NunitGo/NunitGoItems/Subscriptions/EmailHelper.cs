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
        public static bool SingleSend(Address from, Address to, MailMessage message, bool isBodyHtml = true)
        {
            try
            {
                var fromAddress = new MailAddress(from.Email, from.Name);
                var toAddress = new MailAddress(to.Email, to.Name);
                var config = NunitGoHelper.Configuration;
                var smtp = new SmtpClient
                {
                    Host = config.SmtpHost,
                    Port = config.SmtpPort,
                    EnableSsl = config.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Email.Split('@').First(), from.Password),
                    Timeout = 10000
                };
                //Log.Write(String.Format("Sending email from {0}, {1}, {2}, to {3}, {4}. " + Environment.NewLine +
                //                        "Host: {5}, Port: {6}. " + from.Email.Split('@').First(), 
                //                        from.Email, from.Name, from.Password, to.Email, to.Name, smtp.Host, smtp.Port));
                using (smtp)
                {
                    message.From = fromAddress;
                    message.To.Add(toAddress);
                    smtp.Send(message);
                    
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
            NunitGoTest nunitGoTest, string screenshotsPath, bool addLinks, bool isBodyHtml = true)
        {
            foreach (var address in targetEmails)
            {
                var fromMails = mailFromList;
                var success = false;
                while (!success && fromMails.Any())
                {
                    using (var message = new MailMessage
                    {
                        IsBodyHtml = isBodyHtml,
                        Subject = MailGenerator.GetMailSubject(nunitGoTest),
                        Body = MailGenerator.GetMailBody(nunitGoTest, addLinks)
                    })
                    {
                        var attachments = MailGenerator.GetAttachmentsFromScreenshots(nunitGoTest, screenshotsPath);
                        message.AddAttachments(attachments);
                        success = SingleSend(fromMails.First(), address, message, isBodyHtml);
                        if (!success)
                            fromMails = fromMails.Skip(1).ToList();

                    }
                }
            }
        }
    }
}

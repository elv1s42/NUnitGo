using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using NUnitGoCore.NunitGoItems.Events;
using NUnitGoCore.Utils;

namespace NUnitGoCore.NunitGoItems.Subscriptions
{
    internal static class EmailHelper
    {
        private static bool SingleSend(Address from, Address to, MailMessage message)
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
            NunitGoTest nunitGoTest, string screenshotsPath, bool addLinks, 
            bool isEventEmail = false, string eventName = "", TestEvent previousRunEvent = null)
        {
            foreach (var address in targetEmails)
            {
                var fromMails = mailFromList;
                var success = false;
                while (!success && fromMails.Any())
                {
                    using (var message = new MailMessage
                    {
                        IsBodyHtml = true,
                        Subject = MailGenerator.GetMailSubject(nunitGoTest, isEventEmail, eventName),
                        Body = MailGenerator.GetMailBody(nunitGoTest, addLinks, isEventEmail, eventName, previousRunEvent)
                    })
                    {
                        var attachments = MailGenerator.GetAttachmentsFromScreenshots(nunitGoTest, screenshotsPath);
                        message.AddAttachments(attachments);
                        success = SingleSend(fromMails.First(), address, message);
                        if (!success)
                        {
                            fromMails = fromMails.Skip(1).ToList();
                        }

                    }
                }
            }
        }
    }
}

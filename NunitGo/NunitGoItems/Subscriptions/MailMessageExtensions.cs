using System.Collections.Generic;
using System.Net.Mail;

namespace NunitGo.NunitGoItems.Subscriptions
{
    internal static class MailMessageExtensions
    {
        public static void AddAttachments(this MailMessage mail, List<Attachment> list)
        {
            foreach (var attachment in list)
            {
                mail.Attachments.Add(attachment);
            }
        }
    }
}

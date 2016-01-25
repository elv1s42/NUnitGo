using System.Collections.Generic;
using System.Net.Mail;
using NunitGo.NunitGoItems.Subscriptions;

namespace NunitGo.NunitGoItems
{
    public class NunitGoConfiguration
    {
        public string LocalOutputPath;
        public bool TakeScreenshotAfterTestFailed;
        public bool GenerateReport;
        public bool SendEmails;
        public List<Subsciption> Subsciptions;
        public List<SingleTestSubscription> SingleTestSubscriptions;
        public string SmtpHost;
        public int SmtpPort;
        public bool EnableSsl;
        public List<Address> MailFromList;
    }
}

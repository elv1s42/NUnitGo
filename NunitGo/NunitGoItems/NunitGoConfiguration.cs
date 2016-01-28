using System.Collections.Generic;
using NunitGo.NunitGoItems.Subscriptions;

namespace NunitGo.NunitGoItems
{
    public class NunitGoConfiguration
    {
        public string LocalOutputPath;
        public bool TakeScreenshotAfterTestFailed;
        public bool GenerateReport;
        public bool SendEmails;
        public bool AddLinksInsideEmail;
        public List<Subsciption> Subsciptions;
        public List<SingleTestSubscription> SingleTestSubscriptions;
        public string SmtpHost;
        public int SmtpPort;
        public bool EnableSsl;
        public List<Address> SendFromList;

    }
}

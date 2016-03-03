using System.Collections.Generic;
using NunitGoCore.NunitGoItems.Subscriptions;

namespace NunitGoCore.NunitGoItems
{
    public class NunitGoConfiguration
    {
        public string LocalOutputPath;
        public bool TakeScreenshotAfterTestFailed;
        public bool GenerateReport;
        public bool SendEmails;
        public bool AddLinksInsideEmail;
        public string ServerLink;
        public List<Subsciption> Subsciptions;
        public List<SingleTestSubscription> SingleTestSubscriptions;
        public string SmtpHost;
        public int SmtpPort;
        public bool EnableSsl;
        public List<Address> SendFromList;

    }
}

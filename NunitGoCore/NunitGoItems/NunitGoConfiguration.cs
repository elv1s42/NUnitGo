using System.Collections.Generic;
using NUnitGoCore.NunitGoItems.Events;
using NUnitGoCore.NunitGoItems.Subscriptions;

namespace NUnitGoCore.NunitGoItems
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
        public List<EventDurationSubscription> EventDurationSubscriptions;
        public string SmtpHost;
        public int SmtpPort;
        public bool EnableSsl;
        public List<Address> SendFromList;


    }
}

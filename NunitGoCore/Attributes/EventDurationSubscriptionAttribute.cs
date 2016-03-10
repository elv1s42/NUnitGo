using System;
using System.Collections.Generic;
using System.Linq;
using NUnitGoCore.NunitGoItems.Subscriptions;

namespace NUnitGoCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventDurationSubscriptionAttribute : Attribute
    {
        public EventDurationSubscriptionAttribute(string eventName, 
            double maxDifference,
            params string[] emails)
        {
            EventName = eventName;
            MaxDifference = maxDifference;
            var emailsList = emails.ToList();
            Targets = new List<Address>();
            foreach (var email in emailsList)
            {
                Targets.Add(new Address { Email = email });
            }
        }

        public string Name { get; set; }
        public string EventName { get; private set; }
        public double MaxDifference { get; private set; }
        public string FullPath { get; set; }
        public List<Address> Targets { get; private set; }
    }
}

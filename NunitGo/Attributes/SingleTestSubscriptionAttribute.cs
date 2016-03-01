using System;
using System.Collections.Generic;
using System.Linq;
using NunitGo.NunitGoItems.Subscriptions;

namespace NunitGo.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SingleTestSubscriptionAttribute : Attribute
    {
        public bool UnsuccessfulOnly = true;
        public string FullPath { get; set; }
        public List<Address> Targets { private set; get; }

        public SingleTestSubscriptionAttribute(params string[] emails)
        {
            var emailsList = emails.ToList();
            Targets = new List<Address>();
            foreach (var email in emailsList)
            {
                Targets.Add(new Address{Email = email});
            }
        }
    }
}

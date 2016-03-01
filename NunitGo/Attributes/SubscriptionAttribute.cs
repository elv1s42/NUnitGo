using System;
using System.Collections.Generic;
using System.Linq;
using NunitGoCore.NunitGoItems.Subscriptions;

namespace NunitGoCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubscriptionAttribute : Attribute
    {
        public SubscriptionAttribute(params string[] emails)
        {
            var emailsList = emails.ToList();
            Targets = new List<Address>();
            foreach (var email in emailsList)
            {
                Targets.Add(new Address{Email = email});
            }
        }

        public bool UnsuccessfulOnly = true;
        public string Name { get; set; }
        public string FullPath { get; set; }
        public List<Address> Targets { private set; get; }
    }
}

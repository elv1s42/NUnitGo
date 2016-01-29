using System;

namespace NunitGo.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubscriptionAttribute : Attribute
    {
        public bool UnsuccessfulOnly = true;
        public string Name { get; set; }
        public string FullPath { get; set; }
    }
}

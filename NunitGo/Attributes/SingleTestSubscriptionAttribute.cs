using System;

namespace NunitGo.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SingleTestSubscriptionAttribute : Attribute
    {
        public bool UnsuccessfulOnly = true;
        public string FullPath { get; set; }
    }
}

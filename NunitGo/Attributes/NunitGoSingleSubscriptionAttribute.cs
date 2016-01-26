using System;

namespace NunitGo.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NunitGoSingleSubscriptionAttribute : Attribute
    {
        public bool UnsuccessfulOnly = true;
    }
}

using System;

namespace NunitGo.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NunitGoSubscriptionAttribute : Attribute
    {
        public NunitGoSubscriptionAttribute(string name)
        {
            Name = name;
        }

        public bool UnsuccessfulOnly = true;
        public string Name { get; private set; }

    }
}

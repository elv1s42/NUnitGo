using System;

namespace NUnitGoCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestRemarkAttribute : Attribute
    {
        public TestRemarkAttribute(string remarkMessage, string remarkDate)
        {
            RemarkMessage = remarkMessage;
            RemarkDate = DateTime.Parse(remarkDate);
        }

        public TestRemarkAttribute(string remarkMessage, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            RemarkMessage = remarkMessage;
            RemarkDate = new DateTime(year, month, day, hour, minute, second);
        }

        public DateTime RemarkDate { get; }
        public string RemarkMessage { get; }
    }
}

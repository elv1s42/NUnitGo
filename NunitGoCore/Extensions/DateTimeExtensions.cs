using System;

namespace NUnitGoCore.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToJsString(this DateTime date)
        {
            return $@"Date.UTC({date.AddMonths(-1).ToString("yyyy, MM, dd, HH, mm, ss")})";
        }
    }
}

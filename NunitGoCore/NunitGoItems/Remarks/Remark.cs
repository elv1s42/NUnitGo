using System;

namespace NUnitGoCore.NunitGoItems.Remarks
{
    public class Remark
    {
        public Remark(DateTime remarkDate, string remarkMessage)
        {
            RemarkDate = remarkDate;
            RemarkMessage = remarkMessage;
        }

        public DateTime RemarkDate { get; }
        public string RemarkMessage { get; }
    }
}

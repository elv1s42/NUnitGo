using System.Net.Mail;

namespace NUnitGoCore.NunitGoItems.Subscriptions
{
    internal static class AddressExtensions
    {
        public static MailAddress ToMailAddress(this Address address)
        {
            return new MailAddress(address.Email, address.Name);
        }
    }
}

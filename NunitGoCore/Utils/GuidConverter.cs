using System;
using System.Security.Cryptography;
using System.Text;

namespace NUnitGoCore.Utils
{
    public static class GuidConverter
    {
        public static Guid ToMd5HashGuid(string value)
        {
            if (value == null)
                value = string.Empty;
            
            var bytes = Encoding.Default.GetBytes(value);
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(bytes);

            return new Guid(data);
        }
    }
}

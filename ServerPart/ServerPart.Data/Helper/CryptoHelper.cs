using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ServerPart.Data.Helper
{
    public static class CryptoHelper
    {
        public static string GetHash(string source)
        {
            using var md5Hash = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(source);
            var hashBytes = md5Hash.ComputeHash(sourceBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            return hash;
        }
    }
}

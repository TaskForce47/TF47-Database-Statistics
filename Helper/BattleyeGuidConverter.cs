using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TF47DatabaseStatistics.Helper
{
    public static class BattleyeGuidConverter
    {
        public static string ToBattleyeUid(long steamId)
        {
            byte[] parts = { 0x42, 0x45, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte counter = 2;

            do
            {
                parts[counter++] = (byte)(steamId & 0xFF);
            } while ((steamId >>= 8) > 0);

            MD5 md5 = new MD5CryptoServiceProvider();
            var beHash = md5.ComputeHash(parts);
            var sb = new StringBuilder();
            foreach (var t in beHash)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}

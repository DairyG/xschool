using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace XSchool.Helpers
{
    public class EncryptHelper
    {
        public static string MD5Encrypt(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToUpper();
            }
        }
    }
}

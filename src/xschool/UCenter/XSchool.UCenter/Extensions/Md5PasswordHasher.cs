using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XSchool.UCenter.Model;

namespace XSchool.UCenter.Extensions
{
    public class PasswordValidator : Microsoft.AspNetCore.Identity.PasswordValidator<User>
    {
        public async override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var identity = await base.ValidateAsync(manager, user, password);
            return identity;
        }
    }

    public class Md5PasswordHasher : PasswordHasher<User>
    {

        private static readonly string Salt = "7CD955AE-6A04-41BC-952F-0366D2532C95";

        public override string HashPassword(User user, string password)
        {
            if (password.Length < 32)
            {
                using (var md5 = MD5.Create())
                {
                    var pwd = password + "$$$" + Salt;
                    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    var strResult = BitConverter.ToString(result);
                    return strResult.Replace("-", "").ToUpper();
                }
            }
            return password;
        }

        public override PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {

            var result = string.Equals(hashedPassword, providedPassword, StringComparison.CurrentCultureIgnoreCase)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;

            return result;

        }
    }
}

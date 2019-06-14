using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XSchool.UCenter.Model;

namespace XSchool.UCenter.Extensions
{
    public class SignInManager : SignInManager<User>
    {
        public SignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public async override Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (user.State != State.Enable)
            {
                return SignInResult.Failed;
            }
            return await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}

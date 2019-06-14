using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using XSchool.UCenter.Model;

namespace XSchool.UCenter.Extensions
{
    public class ClaimsIdentityFactory : UserClaimsPrincipalFactory<User>
    {
        public ClaimsIdentityFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {

        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("sub", user.UserName.ToString()));
            return identity;
        }
    }
}

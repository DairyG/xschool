using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace XSchool.UCenter.Extensions
{
    public class ClaimsIdentityFactory : UserClaimsPrincipalFactory<IdentityUser<int>>
    {
        public ClaimsIdentityFactory(UserManager<IdentityUser<int>> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser<int> user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("sub", user.UserName.ToString()));
            return identity;
        }
    }
}

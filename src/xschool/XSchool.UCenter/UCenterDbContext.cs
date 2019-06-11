using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XSchool.UCenter
{
    public class UCenterDbContext
       : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UCenterDbContext(DbContextOptions<UCenterDbContext> options):base(options)
        {

        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XSchool.UCenter.Model;

namespace XSchool.UCenter
{
    public class UCenterDbContext
       : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UCenterDbContext(DbContextOptions<UCenterDbContext> options):base(options)
        {

        }
    }
}

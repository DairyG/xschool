using Microsoft.EntityFrameworkCore;
using XShop.GCenter.Model;

namespace XShop.GCenter.Repositories.Extensions
{
    public class GCenterDbContext : DbContext
    {
        public GCenterDbContext(DbContextOptions<GCenterDbContext> options) : base(options) { }

        public DbSet<Company> Company { get; set; }
        public DbSet<BankInfo> bankInfo { get; set; }
        public DbSet<Department> Department { get; set; }

        public DbSet<WorkerInFieldSetting> WorkerInFieldSetting { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.Repositories.Extensions
{
    public class GCenterDbContext : DbContext
    {
        public GCenterDbContext(DbContextOptions<GCenterDbContext> options) : base(options) { }

        public DbSet<Company> Company { get; set; }
        public DbSet<BankInfo> bankInfo { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<WorkerInFieldSetting> WorkerInFieldSetting { get; set; }
        public DbSet<BonusPenaltySetting> BonusPenaltySetting { get; set; }
        public DbSet<PositionSetting> PositionSetting { get; set; }
        public DbSet<Budget> Budget { get; set; }
    }
}

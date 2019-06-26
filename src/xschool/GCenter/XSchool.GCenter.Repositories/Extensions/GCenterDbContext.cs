using Microsoft.EntityFrameworkCore;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.Repositories.Extensions
{
    public class GCenterDbContext : DbContext
    {
        public GCenterDbContext(DbContextOptions<GCenterDbContext> options) : base(options) { }

        public DbSet<WorkerInFieldSetting> WorkerInFieldSetting { get; set; }
        public DbSet<BonusPenaltySetting> BonusPenaltySetting { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Resume> Resume { get; set; }
    }
}

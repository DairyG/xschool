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
        public DbSet<ResumeRecord> ResumeRecord { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<EvaluationType> EvaluationType { get; set; }
        public DbSet<Summary> Summary { get; set; }
        public DbSet<SummaryReply> SummaryReply { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleComplete> ScheduleComplete { get; set; }
    }
}

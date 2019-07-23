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
        public DbSet<BudgetSet> BudgetSet { get; set; }
        public DbSet<BudgetDetails> BudgetDetails { get; set; }
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

        public DbSet<RuleRegulationType> RuleRegulationType { get; set; }
        public DbSet<RuleRegulation> RuleRegulation { get; set; }
        public DbSet<NoteReadRange> NoteReadRange { get; set; }
        public DbSet<RuleRegulationReadRange> RuleRegulationReadRange { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<ScheduleReply> ScheduleReply { get; set; }

        public DbSet<KpiTemplate> KpiTemplate { get; set; }
        public DbSet<KpiTemplateRecord> KpiTemplateRecord { get; set; }

        public DbSet<KpiManageTotal> KpiManageTotal { get; set; }
        public DbSet<KpiManageRecord> KpiManageRecord { get; set; }
        public DbSet<KpiManageDetail> KpiManageDetail { get; set; }

        public DbSet<KpiManageTemplate> KpiManageTemplate { get; set; }
        public DbSet<NoteRead> NoteRead { get; set; }
        public DbSet<RuleRegulationRead> RuleRegulationRead { get; set; }}

using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiTemplateAuditRecordRepository : Repository<KpiTemplateAuditRecord>
    {
        public KpiTemplateAuditRecordRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

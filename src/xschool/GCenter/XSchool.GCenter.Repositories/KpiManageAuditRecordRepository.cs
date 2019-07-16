using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiManageAuditRecordRepository : Repository<KpiManageAuditRecord>
    {
        public KpiManageAuditRecordRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

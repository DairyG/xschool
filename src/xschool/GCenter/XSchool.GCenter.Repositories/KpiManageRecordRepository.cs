using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiManageRecordRepository : Repository<KpiManageRecord>
    {
        public KpiManageRecordRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

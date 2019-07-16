using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiTemplateRecordRepository : Repository<KpiTemplateRecord>
    {
        public KpiTemplateRecordRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

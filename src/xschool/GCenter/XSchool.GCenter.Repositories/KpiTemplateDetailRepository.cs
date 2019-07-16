using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiTemplateDetailRepository : Repository<KpiTemplateDetail>
    {
        public KpiTemplateDetailRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

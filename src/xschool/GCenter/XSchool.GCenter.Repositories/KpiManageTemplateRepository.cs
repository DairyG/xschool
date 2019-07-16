using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiManageTemplateRepository : Repository<KpiManageTemplate>
    {
        public KpiManageTemplateRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

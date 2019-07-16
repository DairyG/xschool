using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiTemplateRepository : Repository<KpiTemplate>
    {
        public KpiTemplateRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class KpiManageTotalRepository : Repository<KpiManageTotal>
    {
        public KpiManageTotalRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class PowerModuleRepository : Repository<PowerModule>
    {
        public PowerModuleRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

using XSchool.Repositories;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.Repositories
{
    public class WorkerInFieldSettingRepository : Repository<WorkerInFieldSetting>
    {
        public WorkerInFieldSettingRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

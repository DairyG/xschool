using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XShop.GCenter.Repositories
{
    public class WorkerInFieldSettingRepository : Repository<WorkerInFieldSetting>
    {
        public WorkerInFieldSettingRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

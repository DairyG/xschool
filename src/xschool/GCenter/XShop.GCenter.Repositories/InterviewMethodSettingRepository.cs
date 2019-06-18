using XSchool.Repositories;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.Repositories
{
    public class InterviewMethodSettingRepository : Repository<InterviewMethodSetting>
    {
        public InterviewMethodSettingRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

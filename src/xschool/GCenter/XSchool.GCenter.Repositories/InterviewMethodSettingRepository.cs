using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class InterviewMethodSettingRepository : Repository<InterviewMethodSetting>
    {
        public InterviewMethodSettingRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

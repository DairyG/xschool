using System;
using XSchool.Repositories;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;

namespace XSchool.GCenter.Repositories
{
    public class EducationInfoRepository : Repository<EducationInfoSetting>
    {
        public EducationInfoRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

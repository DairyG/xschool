using System;
using XSchool.Repositories;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;

namespace XSchool.GCenter.Repositories
{
    public class PropertiesRepository : Repository<PropertiesSetting>
    {
        public PropertiesRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

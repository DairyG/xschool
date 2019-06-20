using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class PositionRepository : Repository<PositionSetting>
    {
        public PositionRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

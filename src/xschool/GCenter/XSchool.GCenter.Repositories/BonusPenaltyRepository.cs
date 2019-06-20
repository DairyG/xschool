using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class BonusPenaltyRepository : Repository<BonusPenaltySetting>
    {
        public BonusPenaltyRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

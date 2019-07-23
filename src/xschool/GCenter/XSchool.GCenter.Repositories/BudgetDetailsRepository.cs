using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class BudgetDetailsRepository : Repository<BudgetDetails>
    {
        public BudgetDetailsRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

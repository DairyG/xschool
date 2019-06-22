using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class BudgetRepository : Repository<Budget>
    {
        public BudgetRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

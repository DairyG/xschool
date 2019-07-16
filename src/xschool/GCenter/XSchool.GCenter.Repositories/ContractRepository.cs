using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class ContractRepository : Repository<Contract>
    {
        public ContractRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

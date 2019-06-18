using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class BankInfoRepository : Repository<BankInfo>
    {
        public BankInfoRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

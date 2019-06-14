using XSchool.Repositories;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.Repositories
{
    public class BankInfoRepository : Repository<BankInfo>
    {
        public BankInfoRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

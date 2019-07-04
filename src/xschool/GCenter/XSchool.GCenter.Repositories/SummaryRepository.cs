using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Repositories
{
    public class SummaryRepository : Repository<Summary>
    {
        public SummaryRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

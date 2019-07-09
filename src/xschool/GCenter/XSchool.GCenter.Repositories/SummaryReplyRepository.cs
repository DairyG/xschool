using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;


namespace XSchool.GCenter.Repositories
{
    public class SummaryReplyRepository : Repository<SummaryReply>
    {
        public SummaryReplyRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class ResumeRecordRepository : Repository<ResumeRecord>
    {
        public ResumeRecordRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

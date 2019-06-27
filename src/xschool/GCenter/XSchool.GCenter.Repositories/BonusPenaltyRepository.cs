using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class TrainingRepository : Repository<Training>
    {
        public TrainingRepository(GCenterDbContext dbContext) : base(dbContext) { }
    }
}

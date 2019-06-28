using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class EvaluationRepository : Repository<Evaluation>
    {
        public EvaluationRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

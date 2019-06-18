using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class DepartmentRepository: Repository<Department>
    {
        public DepartmentRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

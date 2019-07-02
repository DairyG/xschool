using Microsoft.EntityFrameworkCore;
using XSchool.WorkFlow.Model;

namespace XSchool.WorkFlow.Repositories.Extensions
{
    public class WorkFlowDbContext : DbContext
    {
        public WorkFlowDbContext(DbContextOptions<WorkFlowDbContext> options) : base(options) { }


    }
}

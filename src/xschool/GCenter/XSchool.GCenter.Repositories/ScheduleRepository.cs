using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;
using Microsoft.EntityFrameworkCore;

namespace XSchool.GCenter.Repositories
{
    public class ScheduleRepository : Repository<Schedule>
    {
        public ScheduleRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            //_dbContext = dbContext;
        }
    }
}

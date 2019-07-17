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
        private GCenterDbContext _dbContext;
        public ScheduleRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public int UpdatePid(int id)
        {
            Schedule model = new Schedule();
            model.Id = id;
            using (_dbContext)
            {
                _dbContext.Schedule.Attach(model);
                model.Pid = id;
                return _dbContext.SaveChanges();
            }
        }
    }
}

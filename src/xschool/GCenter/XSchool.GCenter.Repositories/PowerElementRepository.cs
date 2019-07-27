using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class PowerElementRepository : Repository<PowerElement>
    {
        private GCenterDbContext _dbContext;
        public PowerElementRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int UpdateBatch(List<int> ids)
        {
            var paramIds = string.Join(",", ids);
            return _dbContext.Database.ExecuteSqlCommand($"UPDATE PowerElement SET Status=0 WHERE Id in ({paramIds})");
        }
    }
}

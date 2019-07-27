using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace XSchool.GCenter.Repositories
{
    public class PowerModuleRepository : Repository<PowerModule>
    {
        private GCenterDbContext _dbContext;
        public PowerModuleRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IPageCollection<PowerModule> Page(int page, int limit, int pid)
        {
            var query = from m1 in _dbContext.PowerModule
                        join m2 in _dbContext.PowerModule on m1.Pid equals m2.Id into pm
                        from pm2 in pm.DefaultIfEmpty()
                        where m1.Status == NomalStatus.Valid
                        orderby m1.Level, m1.DisplayOrder
                        select new PowerModule()
                        {
                            Id = m1.Id,
                            Name = m1.Name,
                            Code = m1.Code,
                            LevelMap = m1.LevelMap,
                            Url = m1.Url,
                            Pid = m1.Pid,
                            PName = pm2.Name,
                            IconName = m1.IconName,
                            Level = m1.Level,
                            Status = m1.Status,
                            IsSystem = m1.IsSystem,
                            DisplayOrder = m1.DisplayOrder
                        };

            query = query.Where(p => p.LevelMap.Contains("," + pid + ","));

            return query.Page(page, limit);
        }
    }
}

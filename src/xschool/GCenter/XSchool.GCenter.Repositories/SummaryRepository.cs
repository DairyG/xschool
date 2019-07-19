using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;
using Microsoft.EntityFrameworkCore;

namespace XSchool.GCenter.Repositories
{
    public class SummaryRepository : Repository<Summary>
    {
        private GCenterDbContext _dbContext;
        public SummaryRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int UpdateRead(int id)
        {
            Summary model = new Summary();
            model.Id = id;
            using (_dbContext)
            {
                _dbContext.Summary.Attach(model);
                model.IsRead = IsRead.Yes;
                return _dbContext.SaveChanges();
            }
        }
        public void UpdateBalance(List<int> channelInt)
        {
            if (channelInt.Count == 0) {
                return;
            }
            var channels = string.Join(",", channelInt);
            var sql = $"UPDATE Summary SET IsRead=2 WHERE Id in ({channels})";
            _dbContext.Database.ExecuteSqlCommand(sql);

        }
    }
}

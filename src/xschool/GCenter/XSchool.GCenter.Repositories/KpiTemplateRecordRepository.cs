using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;

namespace XSchool.GCenter.Repositories
{
    public class KpiTemplateRecordRepository : Repository<KpiTemplateRecord>
    {
        private readonly GCenterDbContext _dbContext;
        public KpiTemplateRecordRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 查询需要生成的数据
        /// </summary>
        /// <param name="kpiId">考核方案</param>
        /// <param name="year">年份</param>
        /// <param name="kpiDate">考核时间</param>
        /// <returns></returns>
        public List<KpiTemplateRecord> QueryByGenerated(KpiPlan kpiId, int year, string kpiDate)
        {
            var query = from tr in _dbContext.KpiTemplateRecord
                        where tr.KpiId == kpiId &
                        !(
                            from mr in _dbContext.KpiManageRecord where mr.KpiId == kpiId & mr.Year == year & mr.KpiDate == kpiDate select mr.KpiTemplateRecordId
                         ).Contains(tr.Id)
                        select tr;
            return query.ToList();
        }
    }
}

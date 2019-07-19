using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Repositories
{
    public class KpiManageDetailRepository : Repository<KpiManageDetail>
    {
        private readonly GCenterDbContext _dbContext;
        public KpiManageDetailRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public object QueryManageDetail(int page, int limit, KpiEvaluationManageQueryDto seach)
        {
            var query = from tr in _dbContext.KpiManageDetail
                        join mr in _dbContext.KpiManageRecord on tr.KpiManageRecordId equals mr.Id
                        where mr.CompanyId == seach.CompanyId & mr.DptId == seach.DptId & mr.EmployeeId == seach.EmployeeId & mr.KpiType == seach.KpiType & mr.KpiId == seach.KpiId & mr.Year == seach.Year & mr.KpiDate == seach.KpiDate
                        orderby tr.Id
                        select new { tr.KpiManageRecordId, tr.EvaluationId, tr.EvaluationName, tr.EvaluationType };

            return query.Page(page, limit);
        }
    }
}

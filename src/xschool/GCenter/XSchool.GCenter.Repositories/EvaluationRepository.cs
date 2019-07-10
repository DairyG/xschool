using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Repositories
{
    public class EvaluationRepository : Repository<Evaluation>
    {
        private GCenterDbContext _dbContext;
        public EvaluationRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IPageCollection<EvaluationDto> Page(int page, int limit, EvaluationSeach seach)
        {
            var query = from e in _dbContext.Evaluation
                        join et in _dbContext.EvaluationType on e.EvaluationTypeId equals et.Id
                        where e.Status == EDStatus.Enable
                        orderby e.Index
                        select new EvaluationDto()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Index = e.Index,
                            EvaluationTypeId = et.Id,
                            EvaluationTypeName = et.Name
                        };

            if (seach.EtId != null)
            {
                query = query.Where(q => q.EvaluationTypeId == seach.EtId);
            }
            if (!string.IsNullOrWhiteSpace(seach.Name))
            {
                query = query.Where(q => q.EvaluationTypeName.Contains(seach.Name));
            }

            return query.Page(page, limit);
        }

    }
}

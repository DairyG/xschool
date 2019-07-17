using System;
using System.Collections.Generic;
using System.Text;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Query.Pageing;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Repositories
{
    public class RuleRegulationTypeRepository : Repository<Model.RuleRegulationType>
    {
        public RuleRegulationTypeRepository(GCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
    public class RuleRegulationRepository : Repository<Model.RuleRegulation>
    {
        private GCenterDbContext _dbContext;
        public RuleRegulationRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IPageCollection<Model.RuleRegulationPage> GetRuleRegulationList(int page, int limit,Model.RuleRegulationSearch search)
        {
            var query = from a in _dbContext.RuleRegulation
                        join b in _dbContext.RuleRegulationType on a.TypeId equals b.Id
                        orderby a.Id descending
                        where (string.IsNullOrEmpty(search.Title)?true:a.Title.Contains(search.Title)) &&(search.TypeId== -1 ? true: search.TypeId == a.TypeId)
                        select new Model.RuleRegulationPage
                        {
                            Id = a.Id,
                            TypeId = a.TypeId,
                            TypeName = b.RuleName,
                            Title = a.Title,
                            PublisherId = a.PublisherId,
                            CreateDate = a.CreateDate,
                            Content = a.Content,
                            EnclosureUrl = a.EnclosureUrl
                        };
            return query.Page(page, limit);
        }
    }
    public class RuleRegulationReadRangeRepository : Repository<Model.RuleRegulationReadRange>
    {
        public RuleRegulationReadRangeRepository(GCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}

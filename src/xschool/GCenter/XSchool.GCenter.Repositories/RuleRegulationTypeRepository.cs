using System;
using System.Collections.Generic;
using System.Text;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

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
        public RuleRegulationRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}

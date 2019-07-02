using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Repositories;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories.Extensions;

namespace XSchool.WorkFlow.Repositories
{
   public  class SubjectTypeRepository : Repository<SubjectType>
    {
        public SubjectTypeRepository(WorkFlowDbContext dbContext) : base(dbContext)
        {

        }
    }
}

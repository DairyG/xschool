using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Repositories;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories.Extensions;

namespace XSchool.WorkFlow.Repositories
{
    public class WorkflowMainRepository : Repository<WorkflowMain>
    {
        private readonly xschool_workflowDbContext _dbContext;
        public WorkflowMainRepository(xschool_workflowDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Repositories;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories.Extensions;

namespace XSchool.WorkFlow.Repositories
{
    public  class WorkflowApprovalStepRepository : Repository<WorkflowApprovalStep>
    {
        public WorkflowApprovalStepRepository(xschool_workflowDbContext dbContext) : base(dbContext)
        {

        }
    }
}

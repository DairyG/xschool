using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Repositories;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using XSchool.WorkFlow.Repositories.Extensions;
using XSchool.Repositories.Extensions;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Repositories
{
    public class WorkflowMainRepository : Repository<WorkflowMain>
    {
        private readonly xschool_workflowDbContext _dbContext;
        public WorkflowMainRepository(xschool_workflowDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<WorkFlowDataPageDto> GetWorkflowMainPageList(WorkFlowDataPageDto model, int pageNum, int pageSize, ref int totalCount, int queryType)
        {
            string sql = @" SELECT a.Id,a.PassStatus,a.BusinessCode,a.Createtime,a.EndTime,a.SubjectName,a.CreateUserId,a.CreateUserName,a.DepId,t.AuditidUserId,t.AuditidUserName FROM                   [WorkflowMain] a
                         OUTER APPLY( 
                         SELECT TOP 1 c.AuditidUserId,c.AuditidUserName FROM [WorkflowApprovalStep]  b,[WorkflowApprovalRecords] c
                         WHERE c.WorkflowApprovalStepId=b.Id AND a.Id=b.WorkflowBusinessId
                         ORDER BY c.Id
                         ) AS T where 1=1 ";
            if (!string.IsNullOrEmpty(model.SubjectName))
            {
                sql += $" and a.SubjectName like '%{model.SubjectName}%' ";
            }
            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                sql += $" and a.BusinessCode like '%{model.BusinessCode}' ";
            }
            if (!string.IsNullOrEmpty(model.CreateUserName))
            {
                sql += $" and a.CreateUserName like '%{model.CreateUserName}' ";
            }

            List<WorkFlowDataPageDto> list = new List<WorkFlowDataPageDto>();
            using (var reader = _dbContext.Database.ExcuteReader(sql))
            {
                while (reader.Read())
                {
                    var obj = new WorkFlowDataPageDto();
                    obj.Id = Convert.ToInt32(reader.DbDataReader["Id"]);
                    obj.PassStatus = (PassStatus)Convert.ToInt32(reader.DbDataReader["PassStatus"]);
                    obj.BusinessCode =reader.DbDataReader["BusinessCode"].ToString();
                    obj.Createtime = DateTime.Parse((reader.DbDataReader["Createtime"]).ToString());
                    if (!string.IsNullOrEmpty(reader.DbDataReader["EndTime"].ToString()))
                    {
                        obj.EndTime =  DateTime.Parse((reader.DbDataReader["EndTime"]).ToString());
                    }
                    obj.SubjectName = reader.DbDataReader["SubjectName"].ToString();
                    obj.DeptId = Convert.ToInt32(reader.DbDataReader["DepId"].ToString());
                    obj.CreateUserId = Convert.ToInt32(reader.DbDataReader["AuditidUserId"].ToString());
                    obj.CreateUserName = reader.DbDataReader["AuditidUserName"].ToString();
                    
                    list.Add(obj);
                }
                return list;
            }

        }

    }
}

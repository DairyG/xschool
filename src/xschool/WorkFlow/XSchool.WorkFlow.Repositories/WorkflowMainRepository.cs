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
        /// <summary>
        ///  待我审核
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> WatiApprovalList(WorkFlowDataPageDto model, int pageIndex, int pageSize, ref int totalCount)
        {
            string query = $@" SELECT ROW_NUMBER() OVER (order by a.Id desc )AS Row,a.Id,a.PassStatus,a.BusinessCode,a.Createtime,a.EndTime,a.SubjectName,a.CreateUserId,a.CreateUserName,a.DepId,t.AuditidUserId,t.AuditidUserName FROM                   [WorkflowMain] a
                         CROSS APPLY( 
                         SELECT TOP 1 c.AuditidUserId,c.AuditidUserName FROM [WorkflowApprovalStep]  b,[WorkflowApprovalRecords] c
                         WHERE  c.WorkflowApprovalStepId=b.Id AND a.Id=b.WorkflowBusinessId  and c.Status=1 and c.AuditidUserId ={model.CreateUserId} ORDER BY c.Id  ) AS T where  a.PassStatus={(int)PassStatus.InApproval} ";
            if (model.Createtime != DateTime.Parse("0001/1/1 0:00:00"))
            {
                query += $" and (a.Createtime>'{model.Createtime}'and a.Createtime<'{model.Createtime.ToShortDateString()+" 23:59:59"}') ";
            }
            if (model.DeptId>0)
            {
                query += $" and a.DepId ={model.DeptId} ";
            }
            if (!string.IsNullOrEmpty(model.SubjectName))
            {
                query += $" and a.SubjectName like '%{model.SubjectName}%' ";
            }
            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                query += $" and a.BusinessCode like '%{model.BusinessCode}' ";
            }
            if (!string.IsNullOrEmpty(model.CreateUserName))
            {
                query += $" and a.CreateUserName like '%{model.CreateUserName}' ";
            }

            totalCount = Convert.ToInt32(_dbContext.Database.ExecuteScalar($"select count(1) from ({query}) TT"));

            string sql = $"SELECT * FROM ({query}) TT WHERE TT.Row between {(pageIndex-1) * pageSize} and {pageIndex * pageSize}";
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
                    obj.CreateUserId = Convert.ToInt32(reader.DbDataReader["CreateUserId"].ToString());
                    obj.CreateUserName = reader.DbDataReader["CreateUserName"].ToString();
                    obj.WaitApprovalId = Convert.ToInt32(reader.DbDataReader["AuditidUserId"].ToString());
                    obj.WaitApprovalName = reader.DbDataReader["AuditidUserName"].ToString();
                    list.Add(obj);
                }
                return list;
            }
        }


        /// <summary>
        ///  我已审批
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> ApprovalRecords(WorkFlowDataPageDto model, int pageIndex, int pageSize, ref int totalCount)
        {
            string query = $@" SELECT ROW_NUMBER() OVER (order by a.Id desc )AS Row,a.Id,a.PassStatus,a.BusinessCode,a.Createtime,a.EndTime,a.SubjectName,a.CreateUserId,a.CreateUserName,a.DepId,t.AuditidUserId,t.AuditidUserName FROM                   [WorkflowMain] a
                         CROSS APPLY( 
                         SELECT TOP 1 c.AuditidUserId,c.AuditidUserName FROM [WorkflowApprovalStep]  b,[WorkflowApprovalRecords] c
                         WHERE c.WorkflowApprovalStepId=b.Id AND a.Id=b.WorkflowBusinessId and (c.Status=-1 or c.Status=2)  ORDER BY c.Id  ) AS T where 1 = 1 and T.AuditidUserId ={model.CreateUserId} ";

            if (model.Createtime != DateTime.Parse("0001/1/1 0:00:00"))
            {
                query += $" and (a.Createtime>'{model.Createtime}'and a.Createtime<'{model.Createtime.ToShortDateString() + " 23:59:59"}') ";
            }
            if (model.DeptId > 0)
            {
                query += $" and a.DepId ={model.DeptId} ";
            }
            if (!string.IsNullOrEmpty(model.SubjectName))
            {
                query += $" and a.SubjectName like '%{model.SubjectName}%' ";
            }
            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                query += $" and a.BusinessCode like '%{model.BusinessCode}' ";
            }
            if (!string.IsNullOrEmpty(model.CreateUserName))
            {
                query += $" and a.CreateUserName like '%{model.CreateUserName}' ";
            }

            totalCount = Convert.ToInt32(_dbContext.Database.ExecuteScalar($"select count(1) from ({query}) TT"));

            string sql = $"SELECT * FROM ({query}) TT WHERE TT.Row between {(pageIndex - 1) * pageSize} and {pageIndex * pageSize}";
            List<WorkFlowDataPageDto> list = new List<WorkFlowDataPageDto>();
            using (var reader = _dbContext.Database.ExcuteReader(sql))
            {
                while (reader.Read())
                {
                    var obj = new WorkFlowDataPageDto();
                    obj.Id = Convert.ToInt32(reader.DbDataReader["Id"]);
                    obj.PassStatus = (PassStatus)Convert.ToInt32(reader.DbDataReader["PassStatus"]);
                    obj.BusinessCode = reader.DbDataReader["BusinessCode"].ToString();
                    obj.Createtime = DateTime.Parse((reader.DbDataReader["Createtime"]).ToString());
                    if (!string.IsNullOrEmpty(reader.DbDataReader["EndTime"].ToString()))
                    {
                        obj.EndTime = DateTime.Parse((reader.DbDataReader["EndTime"]).ToString());
                    }
                    obj.SubjectName = reader.DbDataReader["SubjectName"].ToString();
                    obj.DeptId = Convert.ToInt32(reader.DbDataReader["DepId"].ToString());
                    obj.CreateUserId = Convert.ToInt32(reader.DbDataReader["CreateUserId"].ToString());
                    obj.CreateUserName = reader.DbDataReader["CreateUserName"].ToString();
                    obj.WaitApprovalId = Convert.ToInt32(reader.DbDataReader["AuditidUserId"].ToString());
                    obj.WaitApprovalName = reader.DbDataReader["AuditidUserName"].ToString();
                    list.Add(obj);
                }
                return list;
            }
        }

        /// <summary>
        ///  我发起的
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> MyCreateApproval(WorkFlowDataPageDto model, int pageIndex, int pageSize, ref int totalCount)
        {
            string query = $@" SELECT ROW_NUMBER() OVER (order by a.Id desc )AS Row,a.Id,a.PassStatus,a.BusinessCode,a.Createtime,a.EndTime,a.SubjectName,a.CreateUserId,a.CreateUserName,a.DepId,t.AuditidUserId,t.AuditidUserName FROM                   [WorkflowMain] a
                         CROSS APPLY( 
                         SELECT TOP 1 c.AuditidUserId,c.AuditidUserName FROM [WorkflowApprovalStep]  b,[WorkflowApprovalRecords] c
                         WHERE c.WorkflowApprovalStepId=b.Id AND a.Id=b.WorkflowBusinessId ORDER BY c.Id  ) AS T where a.CreateUserId={model.CreateUserId} ";
            if (model.Createtime != DateTime.Parse("0001/1/1 0:00:00"))
            {
                query += $" and (a.Createtime>'{model.Createtime}'and a.Createtime<'{model.Createtime.ToShortDateString() + " 23:59:59"}') ";
            }
            if (model.DeptId > 0)
            {
                query += $" and a.DepId ={model.DeptId} ";
            }
            if (!string.IsNullOrEmpty(model.SubjectName))
            {
                query += $" and a.SubjectName like '%{model.SubjectName}%' ";
            }
            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                query += $" and a.BusinessCode like '%{model.BusinessCode}' ";
            }
            if (!string.IsNullOrEmpty(model.CreateUserName))
            {
                query += $" and a.CreateUserName like '%{model.CreateUserName}' ";
            }

            totalCount = Convert.ToInt32(_dbContext.Database.ExecuteScalar($"select count(1) from ({query}) TT"));

            string sql = $"SELECT * FROM ({query}) TT WHERE TT.Row between {(pageIndex - 1) * pageSize} and {pageIndex * pageSize}";
            List<WorkFlowDataPageDto> list = new List<WorkFlowDataPageDto>();
            using (var reader = _dbContext.Database.ExcuteReader(sql))
            {
                while (reader.Read())
                {
                    var obj = new WorkFlowDataPageDto();
                    obj.Id = Convert.ToInt32(reader.DbDataReader["Id"]);
                    obj.PassStatus = (PassStatus)Convert.ToInt32(reader.DbDataReader["PassStatus"]);
                    obj.BusinessCode = reader.DbDataReader["BusinessCode"].ToString();
                    obj.Createtime = DateTime.Parse((reader.DbDataReader["Createtime"]).ToString());
                    if (!string.IsNullOrEmpty(reader.DbDataReader["EndTime"].ToString()))
                    {
                        obj.EndTime = DateTime.Parse((reader.DbDataReader["EndTime"]).ToString());
                    }
                    obj.SubjectName = reader.DbDataReader["SubjectName"].ToString();
                    obj.DeptId = Convert.ToInt32(reader.DbDataReader["DepId"].ToString());
                    obj.CreateUserId = Convert.ToInt32(reader.DbDataReader["CreateUserId"].ToString());
                    obj.CreateUserName = reader.DbDataReader["CreateUserName"].ToString();
                    obj.WaitApprovalId = Convert.ToInt32(reader.DbDataReader["AuditidUserId"].ToString());
                    obj.WaitApprovalName = reader.DbDataReader["AuditidUserName"].ToString();
                    list.Add(obj);
                }
                return list;
            }
        }


        /// <summary>
        ///  抄送我的
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> SendCopyMe(WorkFlowDataPageDto model, int pageIndex, int pageSize, ref int totalCount)
        {
            string query = $@" SELECT ROW_NUMBER() OVER (order by a.Id desc )AS Row,a.Id,a.PassStatus,a.BusinessCode,a.Createtime,a.EndTime,a.SubjectName,a.CreateUserId,a.CreateUserName,a.DepId,t.AuditidUserId,t.AuditidUserName FROM                   [WorkflowMain] a
                         CROSS APPLY( 
                         SELECT TOP 1 c.AuditidUserId,c.AuditidUserName FROM [WorkflowApprovalStep]  b,[WorkflowApprovalRecords] c
                         WHERE c.WorkflowApprovalStepId=b.Id AND a.Id=b.WorkflowBusinessId and b.PassType ={(int)PassType.Copy}  ORDER BY c.Id  ) AS T where 1 = 1 and T.AuditidUserId  ={model.CreateUserId}";
            if (model.Createtime != DateTime.Parse("0001/1/1 0:00:00"))
            {
                query += $" and (a.Createtime>'{model.Createtime}'and a.Createtime<'{model.Createtime.ToShortDateString() + " 23:59:59"}') ";
            }
            if (model.DeptId > 0)
            {
                query += $" and a.DepId ={model.DeptId} ";
            }
            if (!string.IsNullOrEmpty(model.SubjectName))
            {
                query += $" and a.SubjectName like '%{model.SubjectName}%' ";
            }
            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                query += $" and a.BusinessCode like '%{model.BusinessCode}' ";
            }
            if (!string.IsNullOrEmpty(model.CreateUserName))
            {
                query += $" and a.CreateUserName like '%{model.CreateUserName}' ";
            }

            totalCount = Convert.ToInt32(_dbContext.Database.ExecuteScalar($"select count(1) from ({query}) TT"));

            string sql = $"SELECT * FROM ({query}) TT WHERE TT.Row between {(pageIndex - 1) * pageSize} and {pageIndex * pageSize}";
            List<WorkFlowDataPageDto> list = new List<WorkFlowDataPageDto>();
            using (var reader = _dbContext.Database.ExcuteReader(sql))
            {
                while (reader.Read())
                {
                    var obj = new WorkFlowDataPageDto();
                    obj.Id = Convert.ToInt32(reader.DbDataReader["Id"]);
                    obj.PassStatus = (PassStatus)Convert.ToInt32(reader.DbDataReader["PassStatus"]);
                    obj.BusinessCode = reader.DbDataReader["BusinessCode"].ToString();
                    obj.Createtime = DateTime.Parse((reader.DbDataReader["Createtime"]).ToString());
                    if (!string.IsNullOrEmpty(reader.DbDataReader["EndTime"].ToString()))
                    {
                        obj.EndTime = DateTime.Parse((reader.DbDataReader["EndTime"]).ToString());
                    }
                    obj.SubjectName = reader.DbDataReader["SubjectName"].ToString();
                    obj.DeptId = Convert.ToInt32(reader.DbDataReader["DepId"].ToString());
                    obj.CreateUserId = Convert.ToInt32(reader.DbDataReader["CreateUserId"].ToString());
                    obj.CreateUserName = reader.DbDataReader["CreateUserName"].ToString();
                    obj.WaitApprovalId = Convert.ToInt32(reader.DbDataReader["AuditidUserId"].ToString());
                    obj.WaitApprovalName = reader.DbDataReader["AuditidUserName"].ToString();
                    list.Add(obj);
                }
                return list;
            }
        }

    }
}

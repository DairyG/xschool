using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using XSchool.WorkFlow.Repositories;
using static XSchool.WorkFlow.Model.Enums;
using System.Linq;
using Logistics.Helpers;
using AutoMapper;
using XSchool.WorkFlow.WebApi.Helper;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 工作流工作具体工作主表
    /// </summary>
    public class WorkflowMainBusiness : Business<WorkflowMain>
    {
        private readonly WorkflowMainRepository _repository;
        private readonly SubjectRepository _repositorySubject;
        private readonly SubjectStepBusiness _subjectStepBusiness;
        private readonly SubjectRepository _subjectRepository;
        private readonly SubjectTypeRepository _repositoryTypeSubject;
        private readonly WorkflowApprovalStepRepository _workflowApprovalStepRepository;
        private readonly SubjectRuleRepository _subjectRuleRepository;
        private readonly WorkflowApprovalRecordsRepository _workflowApprovalRecordsRepository;
        public WorkflowMainBusiness(IServiceProvider provider, WorkflowMainRepository repository, SubjectRepository repositorySubject, SubjectStepBusiness subjectStepBusiness, SubjectRepository subjectRepository, SubjectTypeRepository repositoryTypeSubject, WorkflowApprovalStepRepository workflowApprovalStepRepository, SubjectRuleRepository subjectRuleRepository, WorkflowApprovalRecordsRepository workflowApprovalRecordsRepository) : base(provider, repository)
        {
            this._repository = repository;
            this._repositorySubject = repositorySubject;
            this._subjectStepBusiness = subjectStepBusiness;
            this._subjectRepository = subjectRepository;
            this._repositoryTypeSubject = repositoryTypeSubject;
            this._workflowApprovalStepRepository = workflowApprovalStepRepository;
            this._subjectRuleRepository = subjectRuleRepository;
            this._workflowApprovalRecordsRepository = workflowApprovalRecordsRepository;
        }

        /// <summary>
        ///  根据流程Id获取内容
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public Result GetWorkFlowForm(int SubjectId)
        {
            var data = _repositorySubject.GetSingle(s => s.Id == SubjectId);
            WorkFlowMainFormDto model = new WorkFlowMainFormDto()
            {
                FormContent = data.FormContent,
                FormAttribute = data.FormAttribute,
                SubjectId = data.Id,
                SubjectName = data.SubjectName
            };
            //查询流程节点
            model.SubjectPassList = _subjectStepBusiness.GetDataListBySubjectId(SubjectId);
            return new Result<WorkFlowMainFormDto> { Succeed = true, Data = model };
        }

        /// <summary>
        ///  发起审批
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public Result CreateWork(WorkflowMain model)
        {
            string msg = string.Empty;
            bool status = false;
            try
            {
                if (model.SubjectId <= 0)
                    return new Result() { Succeed = status, Message = "流程参数丢失" };

                var SubjectObj = (from a in _subjectRepository.Entites
                                  join b in _repositoryTypeSubject.Entites on a.SubjectTypeId equals b.Id
                                  select new { b.SubjectTypeName, a.SubjectName }
                                ).FirstOrDefault();

                model.BusinessCode = PingYinHelper.GetFirstSpell(SubjectObj.SubjectTypeName) + "_" + DateTime.Now.ToString("yyMMddHHmmssff") + "_" + new Random().Next(1000, 9999);
                model.SubjectName = SubjectObj.SubjectName;
                //获取流程节点
                var stepList = _subjectStepBusiness.GetDataListBySubjectId(model.SubjectId);
                List<WorkflowApprovalStep> stepEmpList = Mapper.Map<List<WorkflowApprovalStep>>(stepList).OrderBy(s => s.PassNo).ToList();
                //通过流程节点id查询节点对应人
                int i = 0;
                foreach (var item in stepEmpList)
                {
                    i++;
                    var objStep = _subjectRuleRepository.Entites.Where(s => s.SubjectStepId == item.Id).FirstOrDefault();
                    List<WorkflowApprovalRecords> list = new List<WorkflowApprovalRecords>();
                    if (item.PassType != PassType.Summary)
                    {
                        EmployeeDptJobDto modelEmployeeDptJobDto = new EmployeeDptJobDto
                        {
                            CompanyId = objStep.CompanyId,
                            DptId = objStep.DepId,
                            JobId = objStep.JobId,
                            LoadChildDptEmployee = false,
                            OnlySelf = false
                        };
                        //当前节点对应的人员集合(无序)
                        var userList = ApiBusinessHelper.GetEmployeeDptJobByUserIdAsync(modelEmployeeDptJobDto).Result;

                        foreach (var itemChild in userList)
                        {
                            var approvalRecord = new WorkflowApprovalRecords
                            {
                                AuditidUserId = itemChild.userId,
                                AuditidUserName = itemChild.EmployeeName,
                                DataType = 1,
                                Status = 3
                            };
                            if (i == 1 && item.PassType == PassType.Audit)
                            {
                                approvalRecord.Status = 1;
                            }
                            else if (i == 1 && item.PassType == PassType.Copy)
                            {
                                approvalRecord.Status = 2;//抄送人员默认是审核成功（不卡流程）
                            }
                            else if (i > 1 && i < stepEmpList.Count && item.PassType == PassType.Copy)
                            {
                                approvalRecord.Status = 2;//抄送人员默认是审核成功（不卡流程）
                            }
                            list.Add(approvalRecord);
                        }
                    }
                    else
                    {
                        //复盘节点
                        var approvalRecordFP = new WorkflowApprovalRecords
                        {
                            DataType = 1,
                            Status = 3
                        };
                        if (objStep.UserId > 0)
                        {//创建人
                            approvalRecordFP.AuditidUserId = model.CreateUserId;
                            approvalRecordFP.AuditidUserName = model.CreateUserName;
                            list.Add(approvalRecordFP);
                        }

                        if (objStep.DepId > 0)
                        {   //获取部门经理
                            var empLD = ApiBusinessHelper.GetEmployeeManagerByUserId(model.CreateUserId).Result;
                            approvalRecordFP.AuditidUserId = empLD.userId;
                            approvalRecordFP.AuditidUserName = empLD.employeeName;
                            list.Add(approvalRecordFP);
                        }

                    }
                    item.workflowApprovalRecordList = list;
                }


                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    stepEmpList.ForEach(s => s.Id = 0);
                    model.WorkflowApprovalStepList = stepEmpList;
                    status = _repository.Add(model) > 0 ? true : false;
                    ts.Complete();//提交事务
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return new Result() { Succeed = status, Message = msg };
        }

        /// <summary>
        /// 待我审核
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageNum">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> WaitApprove(WorkFlowDataPageDto model,int pageNum, int pageSize,ref int totalCount)
        {
            var query = (from a in _repository.Entites
                                  join b in _workflowApprovalStepRepository.Entites on a.Id equals b.WorkflowBusinessId
                                  join c in _workflowApprovalRecordsRepository.Entites on b.Id equals c.WorkflowApprovalStepId
                                  where a.PassStatus == PassStatus.InApproval && b.PassType != PassType.Copy && c.Status == 1 && c.AuditidUserId == model.CreateUserId
                                  select new WorkFlowDataPageDto
                                  {
                                      PassStatus = a.PassStatus,
                                      BusinessCode = a.BusinessCode,
                                      Createtime = a.Createtime,
                                      EndTime = a.EndTime,
                                      SubjectName = a.SubjectName,
                                      CreateUserId = a.CreateUserId,
                                      CreateUserName = a.CreateUserName,
                                      DeptId = a.DepId,
                                      Id = a.Id,
                                      WaitApprovalId = c.AuditidUserId,
                                      WaitApprovalName = c.AuditidUserName
                                  });
            totalCount = query.Count();
              var subjectObjList = query.Skip(pageSize * (pageNum - 1)).Take(pageSize).OrderBy(s=>s.Createtime).ToList();

            return  subjectObjList;
        }

    }
}

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
                                AudioStatus = AudioStatus.UnApprovalInfo
                            };
                            if (i == 1 && item.PassType == PassType.Audit)
                            {
                                approvalRecord.AudioStatus = AudioStatus.WaitAgree;
                            }
                            else if (i == 1 && item.PassType == PassType.Copy)
                            {
                                approvalRecord.AudioStatus = AudioStatus.Agree;//抄送人员默认是审核成功（不卡流程）
                            }
                            else if (i > 1 && i < stepEmpList.Count && item.PassType == PassType.Copy)
                            {
                                approvalRecord.AudioStatus = AudioStatus.Agree;//抄送人员默认是审核成功（不卡流程）
                            }
                            approvalRecord.ReadStatus = 1;
                            list.Add(approvalRecord);
                        }
                    }
                    else
                    {
                        //复盘节点
                        var approvalRecordFP = new WorkflowApprovalRecords
                        {
                            DataType = 1,
                            AudioStatus = AudioStatus.UnApprovalInfo
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

               //stepEmpList.Where(s => s.PassNo == 1).ToList().ForEach(s => s.AuditNo = true);
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
        public List<WorkFlowDataPageDto> WatiApprovalList(WorkFlowDataPageDto model, int pageNum, int pageSize, ref int totalCount)
        {
            var subjectObjList = _repository.WatiApprovalList(model, pageNum, pageSize, ref totalCount);
            return subjectObjList;
        }

        /// <summary>
        /// 我已审批
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageNum">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> ApprovalRecords(WorkFlowDataPageDto model, int pageNum, int pageSize, ref int totalCount)
        {
            var subjectObjList = _repository.ApprovalRecords(model, pageNum, pageSize, ref totalCount);
            return subjectObjList;
        }


        /// <summary>
        /// 我发起的
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageNum">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> MyCreateApproval(WorkFlowDataPageDto model, int pageNum, int pageSize, ref int totalCount)
        {
            var subjectObjList = _repository.MyCreateApproval(model, pageNum, pageSize, ref totalCount);
            return subjectObjList;
        }

        /// <summary>
        /// 抄送我的
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageNum">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<WorkFlowDataPageDto> SendCopyMe(WorkFlowDataPageDto model, int pageNum, int pageSize, ref int totalCount)
        {
            var subjectObjList = _repository.SendCopyMe(model, pageNum, pageSize, ref totalCount);
            return subjectObjList;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Revoke(int Id, ref string msg)
        {
            bool status = false;
            try
            {
                status = _repository.Update(s => s.Id == Id, s => new WorkflowMain { PassStatus = PassStatus.Cancel });
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return status;
        }

        /// <summary>
        ///  审核人员变更
        /// </summary>
        /// <returns></returns>
        public Result ApprovalPerson(ApprovalPersonChageDto model, string optName)
        {
            bool status = false;
            string msg = "";
            try
            {
                var dataObj = _repository.GetData(model.Id);
                status = _workflowApprovalRecordsRepository.Update(s => s.Id == dataObj.WorkflowApprovalStepId, s => new WorkflowApprovalRecords { AuditidUserName = model.AuditidUserName, AuditidUserId = model.AuditidUserId });
                WorkflowApprovalRecords addData = new WorkflowApprovalRecords()
                {
                    AuditidTime = DateTime.Now,
                    WorkflowApprovalStepId = dataObj.WorkflowApprovalStepId,
                    DataType = 2,
                    Memo = optName + "将审批人从" + dataObj.AuditidUserName + "换为" + model.AuditidUserName
                };

                status = _workflowApprovalRecordsRepository.Add(addData) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                status = false;
                msg = ex.Message.ToString();
            }
            return new Result { Succeed = status, Message = msg };
        }
        /// <summary>
        ///  获取审核进度
        /// </summary>
        /// <returns></returns>
        public Result GetApprovalInfo(int Id)
        {
            var data = from a in _workflowApprovalStepRepository.Entites
                       join b in _workflowApprovalRecordsRepository.Entites
                       on a.Id equals b.WorkflowApprovalStepId
                       where a.WorkflowBusinessId == Id && b.DataType == 1
                       select new WorkFlowStepersInfoDto
                       {
                           AuditNo = a.AuditNo,
                           PassName = a.PassName,
                           PassNo = a.PassNo,
                           PassType = a.PassType,
                           SubjectRulePassList = a.workflowApprovalRecordList.Select(s => new WorkflowApprovalStepRecordsDto()
                           {
                               AuditidUserId = s.AuditidUserId,
                               AuditidUserName = s.AuditidUserName,
                               AudioStatus = s.AudioStatus,
                               ReadStatus = s.ReadStatus
                           }).ToList()
                       };
            var dataResult = data.ToList();
            return new Result<List<WorkFlowStepersInfoDto>>() { Succeed = true, Data = dataResult };
        }

        /// <summary>
        /// 同意/不同意/驳回
        /// </summary>
        /// <param name="Id">业务流程主键Id</param>
        /// <param name="AudioStatus">审批状态 -2 驳回，-1 拒绝，2 同意</param>
        /// <param name="Memo">审核意见</param>
        ///  <param name="userId">当前操作人</param>
        ///   <param name="msg">异常信息</param>
        /// <returns></returns>
        public bool ApprovaIsAgree(int Id, AudioStatus AudioStatus, string Memo, int userId, ref string msg)
        {
            bool status = false;
            var rel = new Result();
            //查询当前审核节点及审核人信息
            var stepApprovaObj = (from a in _workflowApprovalStepRepository.Entites
                                  join b in _workflowApprovalRecordsRepository.Entites
                                  on a.Id equals b.WorkflowApprovalStepId
                                  where a.WorkflowBusinessId == Id && a.AuditNo == true && b.DataType == 1
                                  select new WorkflowApprovalStep
                                  {
                                      Id = Id,
                                      AuditNo = a.AuditNo,
                                      PassName = a.PassName,
                                      PassNo = a.PassNo,
                                      PassType = a.PassType,
                                      IsCountersign = a.IsCountersign,
                                      IsEnd = a.IsEnd,
                                      workflowApprovalRecordList = a.workflowApprovalRecordList.Select(s => new WorkflowApprovalRecords()
                                      {
                                          Id = s.Id,
                                           AudioStatus=s.AudioStatus,
                                          AuditidUserId = s.AuditidUserId,
                                          AuditidUserName = s.AuditidUserName
                                      }).ToList()
                                  }).FirstOrDefault();
            //验证当前审核人信息
            var obj = stepApprovaObj.workflowApprovalRecordList.Where(s => s.AudioStatus == AudioStatus.WaitAgree && s.AuditidUserId == userId).FirstOrDefault();
            if (obj == null)
            {
                msg = "审核信息验证未通过，请联系管理员";
                return status;
            }
            //根据条件查询，此节点类型只可能为Audit（审核节点）或 Summary（复盘节点）
            if (AudioStatus == AudioStatus.Reject)
            {
                //驳回
                status = Reject(stepApprovaObj, obj, ref msg);
                return status;
            }
            //修改审核人的审核状态(修改缓存和数据库)
            stepApprovaObj.workflowApprovalRecordList.Where(s => s.AudioStatus == AudioStatus.WaitAgree && s.AuditidUserId == userId).ToList().ForEach(s => s.AudioStatus = AudioStatus);
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    status = _workflowApprovalRecordsRepository.Update(s => s.Id == obj.Id, s => new WorkflowApprovalRecords { AudioStatus = AudioStatus, AuditidTime = DateTime.Now, Memo = Memo });
                    //最后一个节点
                    if (stepApprovaObj.IsEnd)
                    {
                        //判断当前节点是否还有人未审核
                        var hasApprovalerCount = stepApprovaObj.workflowApprovalRecordList.Where(s => s.AudioStatus == AudioStatus.WaitAgree).Count();
                        if (hasApprovalerCount == 0)
                        {
                            status = _repository.Update(s => s.Id == Id, s => new WorkflowMain { PassStatus = AudioStatus == AudioStatus.Agree ? PassStatus.Agree : PassStatus.DisAgree, EndTime = DateTime.Now });
                        }
                    }
                    else
                    {
                        bool GoNextNo = false;//true 是否跳转至下个节点
                        if (stepApprovaObj.IsCountersign)
                        { //会签
                          //判断当前节点是否还有人未审核
                            var hasApprovalerCount = stepApprovaObj.workflowApprovalRecordList.Where(s => s.AudioStatus == AudioStatus.WaitAgree).Count();
                            if (hasApprovalerCount == 0)
                            {
                                GoNextNo = true;
                            }
                        }
                        else
                        { //或签
                            GoNextNo = true;
                        }
                        if (GoNextNo)
                        {
                            //更改下个节点为待审核状态
                            var nextStepNoObj = _workflowApprovalStepRepository.Entites.Where(s => s.Id == Id && s.PassNo > stepApprovaObj.PassNo && s.PassType != PassType.Copy).OrderBy(s => s.PassNo).FirstOrDefault();
                            status = _workflowApprovalStepRepository.Update(s => s.Id == stepApprovaObj.Id, s => new WorkflowApprovalStep { AuditNo = false });
                            status = _workflowApprovalStepRepository.Update(s => s.Id == nextStepNoObj.Id, s => new WorkflowApprovalStep { AuditNo = true });
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                if (string.IsNullOrEmpty(msg) && status)
                {
                    ts.Complete();//提交事务}
                }
            }
            return status;
        }
        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="stepApprovaObj">当前节点信息</param>
        /// <param name="recordsobj">当前审核人信息</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool Reject(WorkflowApprovalStep stepApprovaObj, WorkflowApprovalRecords recordsobj, ref string msg)
        {
            bool status = false;
            //添加当前节点审核的操作记录
            WorkflowApprovalRecords objRecords = new WorkflowApprovalRecords
            {
                DataType = 2,
                AuditidTime = DateTime.Now,
                Memo = "审批人 " + recordsobj.AuditidUserName + " 驳回了申请",
                WorkflowApprovalStepId = stepApprovaObj.Id
            };
            status = _workflowApprovalRecordsRepository.Add(objRecords) > 0 ? true : false;

            //修改当前节点的节点状态和人员审核状态
            status = _workflowApprovalStepRepository.Update(s => s.Id == stepApprovaObj.Id, s => new WorkflowApprovalStep { AuditNo = false });
            _workflowApprovalRecordsRepository.Update(s => s.WorkflowApprovalStepId == stepApprovaObj.Id && s.DataType == 1, s => new WorkflowApprovalRecords { AudioStatus = AudioStatus.UnApprovalInfo, AuditidTime = null });
            //修改上个节点的节点状态和人员审核状态
            var prevPassNo = _workflowApprovalStepRepository.Entites.Where(s => s.PassType != PassType.Copy && s.WorkflowBusinessId == stepApprovaObj.WorkflowBusinessId && s.PassNo < stepApprovaObj.PassNo).OrderByDescending(s => s.PassNo).FirstOrDefault();

            status = _workflowApprovalStepRepository.Update(s => s.Id == prevPassNo.Id, s => new WorkflowApprovalStep { AuditNo = true });

            status = _workflowApprovalRecordsRepository.Update(s => s.WorkflowApprovalStepId == prevPassNo.Id && s.DataType == 1, s => new WorkflowApprovalRecords { AudioStatus = AudioStatus.WaitAgree, AuditidTime = null });
            return status;
        }


    }
}

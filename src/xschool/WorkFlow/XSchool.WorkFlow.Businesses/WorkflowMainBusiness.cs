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

        public WorkflowMainBusiness(IServiceProvider provider, WorkflowMainRepository repository, SubjectRepository repositorySubject, SubjectStepBusiness subjectStepBusiness, SubjectRepository subjectRepository, SubjectTypeRepository repositoryTypeSubject, WorkflowApprovalStepRepository workflowApprovalStepRepository) : base(provider, repository)
        {
            this._repository = repository;
            this._repositorySubject = repositorySubject;
            this._subjectStepBusiness = subjectStepBusiness;
            this._subjectRepository = subjectRepository;
            this._repositoryTypeSubject = repositoryTypeSubject;
            this._workflowApprovalStepRepository = workflowApprovalStepRepository;
        }

        /// <summary>
        ///  根据流程Id获取内容
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public Result GetWorkFlowForm(int SubjectId)
        {
            var data= _repositorySubject.GetSingle(s => s.Id == SubjectId);
            WorkFlowMainFormDto model = new WorkFlowMainFormDto()
            {
                FormContent = data.FormContent,
                FormAttribute = data.FormAttribute,
                SubjectId=data.Id,
                SubjectName=data.SubjectName
            };
            //查询流程节点
            model.SubjectPassList = _subjectStepBusiness.GetDataListBySubjectId(SubjectId);
            return new Result<WorkFlowMainFormDto> { Succeed = true,  Data = model };
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
                return new Result() { Succeed = status,  Message="流程参数丢失" };

            var SubjectObj = (from a in _subjectRepository.Entites
                              join b in _repositoryTypeSubject.Entites  on  a.SubjectTypeId equals b.Id
                              select new { b.SubjectTypeName,a.SubjectName }
                            ).FirstOrDefault();

                model.BusinessCode = PingYinHelper.GetFirstSpell(SubjectObj.SubjectTypeName) + "_" + DateTime.Now.ToString("yyMMddHHmmssff") + "_" + new Random().Next(1000, 9999);
               model.SubjectName = SubjectObj.SubjectName;
                //获取流程节点
                var stepList = _subjectStepBusiness.GetDataListBySubjectId(model.SubjectId);
                List<WorkflowApprovalStep> stepEmpList = Mapper.Map<List<WorkflowApprovalStep>>(stepList);
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    stepEmpList.ForEach(s => s.Id = 0);
                    model.WorkflowApprovalStepList = stepEmpList;
                    status = _repository.Add(model) > 0 ? true : false;
                  //  stepEmpList.ForEach(s => s.WorkflowBusinessId = model.Id);
                  // _workflowApprovalStepRepository.AddRange(stepEmpList);



                    ts.Complete();//提交事务
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return new Result(){ Succeed = status, Message= "失败:"+msg };
        }

        /// <summary>
        ///  待我审批
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public Result WaitApprove(WorkflowMain model)
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
                status = _repository.Add(model) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return new Result() { Succeed = status, Message = "失败:" + msg };
        }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using XSchool.WorkFlow.Repositories;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 流程管理
    /// </summary>
    public class SubjectBusiness : Business<Subject>
    {
        private readonly SubjectRepository _repository;
        private readonly SubjectRuleRepository _rulerepository;
        private readonly SubjectStepRepository _steprepository;
        private readonly SubjectTypeRepository _repositoryTypeSubject;
        public SubjectBusiness(IServiceProvider provider, SubjectRepository repository, SubjectRuleRepository rulerepository, SubjectStepRepository steprepository, SubjectTypeRepository repositoryTypeSubject)
            : base(provider, repository)
        {
            this._repository = repository;
            this._rulerepository = rulerepository;
            this._steprepository = steprepository;
            this._repositoryTypeSubject = repositoryTypeSubject;
        }
        /// <summary>
        /// /添加流程管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result Add(Subject model)
        {
            if (model.SubjectRuleRangeList == null || model.SubjectStepFlowList == null)
            {
                return new Result() { Succeed = false, Message = "参数错误!" };
            }
            string msg = string.Empty;
            bool status = false;
            try
            {
                status = _repository.Add(model) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            var dataResult = new Result() { Succeed = status, Message = msg };
            return dataResult;
        }
        /// <summary>
        /// 修改流程管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result Edit(Subject model)
        {
            string msg = string.Empty;
            bool status = false;
            try
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    //主表
                    status = _repository.Update(s => s.Id == model.Id, s => new Subject
                    {
                        FlowTypeId = model.FlowTypeId,
                        FormContent = model.FormContent,
                        FormAttribute = model.FormAttribute,
                        IcoUrl = model.IcoUrl,
                        PassInfo = model.PassInfo,
                        SubjectName = model.SubjectName,
                        UpdateTime = DateTime.Now,
                        SubjectTypeId = model.SubjectTypeId
                    });
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //根据流程id删除对应可视范围
                    status = _rulerepository.Delete(s => s.SubjectId == model.Id&&s.BusinessType==BusinessType.Transaction) > 0 ? true : false;
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //根据节点id删除对应节点人员表
                    var stepList = _steprepository.Query(s => s.SubjectId == model.Id);
                    var SubjectIds = (from b in stepList select b.Id).ToArray();
                    status = _rulerepository.Delete(s => SubjectIds.Contains(s.SubjectStepId)) > 0 ? true : false;
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //根据流程id删除对应节点
                    status = _steprepository.Delete(s => s.SubjectId == model.Id) > 0 ? true : false;
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //添加可视范围
                    var subjectRuleList = model.SubjectRuleRangeList as List<SubjectRule>;
                    status = _rulerepository.AddRange(subjectRuleList) > 0 ? true : false;
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //添加流程节点
                    var subjectStepList = model.SubjectStepFlowList as List<SubjectStep>;
                    status = _steprepository.AddRange(subjectStepList) > 0 ? true : false;
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };

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
        /// /获取流程对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SubjectDto GetSubjectById(int Id)
        {

            var query = (from a in _repository.Entites
                         join b in _rulerepository.Entites
                         on a.Id equals b.SubjectId into ruleRangeList
                         join c in _steprepository.Entites
                         on a.Id equals c.SubjectId into stepRangeList
                         where a.Id == Id
                         select new SubjectDto
                         {
                             FlowTypeId = a.FlowTypeId,
                             FormContent = a.FormContent,
                             FormAttribute = a.FormAttribute,
                             IcoUrl = a.IcoUrl,
                             Id = a.Id,
                             Remark=a.Remark,
                             PassInfo = a.PassInfo,
                             SubjectName = a.SubjectName,
                             SubjectTypeId = a.SubjectTypeId,
                             SubjectRuleRangeList = ruleRangeFun(ruleRangeList),
                             SubjectStepFlowList = stepRangeList.Select(q => new SubjectStepDto
                             {
                                 IsCountersign = q.IsCountersign,
                                 IsEnd = q.IsEnd,
                                 PassName = q.PassName,
                                 PassNo = q.PassNo,
                                 PassType = q.PassType,
                                 SubjectRulePassList = _rulerepository.Entites.Where(s => s.SubjectStepId == q.Id).Select(s => new SubjectRuleDto
                                 {
                                     CompanyId = s.CompanyId,
                                     DepId = s.DepId,
                                     JobDepId = s.JobDepId,
                                     JobId = s.JobId,
                                     dataType = s.dataType,
                                     UserId = s.UserId
                                 }).ToList()
                             }).OrderBy(s => s.PassNo).ToList()
                         }).FirstOrDefault();
            return query;
        }
        /// <summary>
        /// 流程可视人范围
        /// </summary>
        /// <param name="subjectRulesList"></param>
        /// <returns></returns>
        private List<SubjectRuleDto> ruleRangeFun(IEnumerable<SubjectRule> subjectRulesList)
        {
            var list = subjectRulesList.Select(p => new SubjectRuleDto
            {
                CompanyId = p.CompanyId,
                CompanyName = p.CompanyName,
                DepId = p.DepId,
                DepName = p.DepName,
                JobDepId = p.JobDepId,
                JobDepName = p.JobDepName,
                JobId = p.JobId,
                JobName = p.JobName,
                UserId = p.UserId,
                dataType = p.dataType,
                UserName = p.UserName
            }).ToList();
            return list;
        }
        /// <summary>
        /// 启用或禁用流程
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status">1禁用，2启用</param>
        /// <returns></returns>
        public Result EnableSubject(int Id, int status)
        {
            string msg = string.Empty;
            var result = _repository.Update(s => s.Id == Id, s => new Subject { Status = (EDStatus)status });
            if (!result)
            {
                msg = "操作失败。。。";
            }
            return new Result() { Message = msg, Succeed = result };
        }

        /// <summary>
        /// 获取流程分组及流程内容
        /// </summary>
        /// <param name="enableStatus">默认0查询所有，1查询启用的</param>
        /// <returns></returns>
        public Result GetSubject()
        {
            var dataSubject = GetSubjectData(0);
            foreach (subjectTypeDto itemParets in dataSubject)
            {
                if (itemParets.subjectList.Count == 0) continue;
                foreach (subjectViewDto item in itemParets.subjectList)
                {
                    item.SubjectRuleList = _rulerepository.Entites.Where(s => s.BusinessType == BusinessType.Transaction && s.SubjectId == item.subjectId).Select(p => new
                  SubjectRuleDto
                    {
                        CompanyId = p.CompanyId,
                        CompanyName = p.CompanyName,
                        DepId = p.DepId,
                        DepName = p.DepName,
                        JobDepId = p.JobDepId,
                        JobDepName = p.JobDepName,
                        JobId = p.JobId,
                        JobName = p.JobName,
                        SubjectStepId = p.SubjectStepId,
                        UserId = p.UserId,
                        UserName = p.UserName,
                        dataType = p.dataType
                    }).ToList();
                }

            }

            var dataresult = dataSubject;
            return new Result<List<subjectTypeDto>>() { Data = dataresult, Succeed = true };
        }

        /// <summary>
        /// 默认0查询所有，1查询启用的
        /// </summary>
        /// <param name="enableStatus"></param>
        /// <returns></returns>
        public List<subjectTypeDto> GetSubjectData(int enableStatus)
        {
            List<subjectTypeDto> empData = null;
            if (enableStatus == 1)
            {
                empData = (from a in _repositoryTypeSubject.Entites
                           join b in _repository.Entites on a.Id equals b.SubjectTypeId into subjectList
                           select new subjectTypeDto
                           {
                               Id = a.Id,
                               SubjectTypeName = a.SubjectTypeName,
                               subjectList = subjectList.Where(s => s.Status == EDStatus.Enable).Select(q => new subjectViewDto
                               {
                                   subjectId = q.Id,
                                   SubjectName = q.SubjectName,
                                   UpdateTime = q.UpdateTime == null ? "" : q.UpdateTime.ToString(),
                                  Remark = q.Remark
                     }).ToList()
                 }).ToList();
            }
            else
            {
                empData = (from a in _repositoryTypeSubject.Entites
                           join b in _repository.Entites on a.Id equals b.SubjectTypeId into subjectList
                           select new subjectTypeDto
                           {
                               Id = a.Id,
                               SubjectTypeName = a.SubjectTypeName,
                               subjectList = subjectList.Select(q => new subjectViewDto
                               {
                                   subjectId = q.Id,
                                   SubjectName = q.SubjectName,
                                   UpdateTime = q.UpdateTime == null ? "" : q.UpdateTime.ToString(),
                                   Remark = q.Remark
                               }).ToList()
                           }).ToList();
            }
            return empData;
        }

        /// <summary>
        /// 获取所有启用的流程分组及流程内容(发起审批)
        /// </summary>
        /// <returns></returns>
        public Result GetEnableSubject()
        {
            var dataSubject = GetSubjectData(1);
            return new Result<List<subjectTypeDto>>() { Data = dataSubject, Succeed = true };
        }

        /// <summary>
        /// 修改流程可见范围
        /// </summary>
        /// <returns></returns>
        public Result UpdateSubjectRange(SubjectRangeDto model)
        {
            string msg = string.Empty;
            bool status = false;
            try
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    status =_rulerepository.Delete(s => s.SubjectId == model.SubjectId)>0?true:false;
                    if (status)
                    {
                        model.SubjectRuleRangeList.ForEach(s => s.SubjectId = model.SubjectId);
                        List<SubjectRule> rangeList = Mapper.Map<List<SubjectRule>>(model.SubjectRuleRangeList);
                        status=_rulerepository.AddRange(rangeList) > 0 ? true : false;
                    }

                    ts.Complete();//提交事务
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return new Result() { Succeed = status, Message = msg };
        }
    }
}
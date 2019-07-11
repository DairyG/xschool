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
        public SubjectBusiness(IServiceProvider provider, SubjectRepository repository, SubjectRuleRepository rulerepository, SubjectStepRepository steprepository)
            : base(provider, repository)
        {
            this._repository = repository;
            this._rulerepository = rulerepository;
            this._steprepository = steprepository;
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
            return new Result() { Succeed = status, Message = msg };
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
                        SubjectTypeId = model.SubjectTypeId
                    });
                    if (!status) return new Result() { Succeed = status, Message = "参数修改失败" };
                    //根据流程id删除对应可视范围
                    status = _rulerepository.Delete(s => s.SubjectId == model.Id) > 0 ? true : false;
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
                DepId = p.DepId,
                JobDepId = p.JobDepId,
                JobId = p.JobId,
                UserId = p.UserId
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 节点人员表、流程可视范围人员
    /// </summary>
    public class SubjectRuleBusiness : Business<SubjectRule>
    {
        private readonly SubjectRuleRepository _repository;
        public SubjectRuleBusiness(IServiceProvider provider, SubjectRuleRepository repository)
            : base(provider, repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 根据流程id删除对应可视范围
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public bool DeleteBySubjectId(int subjectId)
        {
            return _repository.Delete(s => s.SubjectId == subjectId) > 0 ? true : false;
        }

        /// <summary>
        /// 根据节点id删除对应节点人员表
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public bool Delete(int[] SubjectIds)
        {
            return _repository.Delete(SubjectIds) > 0 ? true : false;
        }
    }
}

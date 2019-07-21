using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;
using System.Linq;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 科目管理节点
    /// </summary>
    public class SubjectStepBusiness : Business<SubjectStep>
    {
        private readonly SubjectStepRepository _repository;
        public SubjectStepBusiness(IServiceProvider provider, SubjectStepRepository repository)
            : base(provider, repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 根据流程id删除对应节点
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public bool DeleteBySubjectId(int subjectId)
        {
            return _repository.Delete(s => s.SubjectId == subjectId) > 0 ? true : false;
        }

        /// <summary>
        /// 根据流程id查询对应节点
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public IList<SubjectStep> GetDataListBySubjectId(int subjectId)
        {
           return  _repository.Query(s => s.SubjectId == subjectId).OrderBy(s=>s.PassNo).ToList();
        }

    }
}

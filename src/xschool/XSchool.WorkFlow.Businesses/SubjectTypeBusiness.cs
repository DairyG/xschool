using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;

namespace XSchool.WorkFlow.Businesses
{
    public class SubjectTypeBusiness : Business<SubjectType>
    {
        private readonly SubjectTypeRepository _repository;
        public SubjectTypeBusiness(IServiceProvider provider, SubjectTypeRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// /验证流程组别名称重复
        /// </summary>
        /// <param name="SubjectTypeName"></param>
        /// <returns></returns>
        public bool IsExist(string SubjectTypeName)
        {
            var model = Result.Success(_repository.GetSingle(p => p.SubjectTypeName == SubjectTypeName)).Data;
            return model == null ? true : false;
        }
    }
}

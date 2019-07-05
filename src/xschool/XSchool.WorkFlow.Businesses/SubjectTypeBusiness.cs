using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 流程组别
    /// </summary>
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
        public SubjectType IsExist(string SubjectTypeName)
        {
            var model =_repository.GetSingle(p => p.SubjectTypeName == SubjectTypeName);
            return model;
        }
        /// <summary>
        /// /添加组别
        /// </summary>
        /// <param name="SubjectTypeName"></param>
        /// <returns></returns>
        public Result AddOrEdit(SubjectType model)
        {
            string msg = string.Empty;
            if (IsExist(model.SubjectTypeName)!=null)
            {
                return new Result() { Message= "流程名称重复!", Succeed= false };
            }

            if (model.Id > 0)
            {
                msg = _repository.Update(model) > 0 ? "" : "编辑失败！";
            }
            else
            {
                msg = _repository.Add(model) > 0 ? "" :"添加失败";
            }
            return new Result() { Message = msg, Succeed = string.IsNullOrEmpty(msg)?true:false };
        }
        /// <summary>
        /// /获取启用的所有流程组别
        /// </summary>
        /// <returns></returns>
        public IList<SubjectType> GetSubjectTypeList()
        {
           var subjectTypeList=  _repository.Query(s=>s.Status== EDStatus.Enable);
            return subjectTypeList;
        }

    }
}

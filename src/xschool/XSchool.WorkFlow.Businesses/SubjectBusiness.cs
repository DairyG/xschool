using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;
using XSchool.WorkFlow.WebApi.ViewModel;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 流程管理
    /// </summary>
    public class SubjectBusiness : Business<Subject>
    {
        private readonly SubjectRepository _repository;
        public SubjectBusiness(IServiceProvider provider, SubjectRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// /添加流程管理
        /// </summary>
        /// <param name="SubjectTypeName"></param>
        /// <returns></returns>
        public Result AddOrEdit(SubjectDto model)
        {
            string msg = string.Empty;
            bool status = false;
            if (model.Subject.Id > 0)
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {

                    ////流程主表
                    //_repository.Add(model.Subject);
                    ////节点表
                    //_repository.Add(subjectModel);
                    ////节点详情表
                    //_repository.Add(subjectModel);
                    ts.Complete();//提交事务
                }
                
            }
            else
            {   //主表
                //_repository.Update(subjectModel);
                ////节点表
                //_repository.Update(subjectModel);
                ////节点详情表
                //_repository.Update(subjectModel);
            }
            return new Result() { Succeed=status,Message= msg };
        }
        /// <summary>
        /// /查询流程管理
        /// </summary>
        /// <param name="SubjectTypeName"></param>
        /// <returns></returns>
        public Result GetSubjectModelById(SubjectType model)
        {
            return new Result();
        }


    }
}

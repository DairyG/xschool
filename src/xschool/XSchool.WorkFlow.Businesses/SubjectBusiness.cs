using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Repositories;

namespace XSchool.WorkFlow.Businesses
{
    /// <summary>
    /// 流程管理
    /// </summary>
    public class SubjectBusiness : Business<Subject>
    {
        private readonly SubjectRepository repository;
        public SubjectBusiness(IServiceProvider provider, SubjectRepository _repository) : base(provider, _repository)
        {
            this.repository = _repository;
        }
        /// <summary>
        /// /添加流程管理
        /// </summary>
        /// <param name="SubjectTypeName"></param>
        /// <returns></returns>
        public Result AddOrEdit(Subject subjectModel)
        {
            string msg = string.Empty;
            bool status = false;
            if (subjectModel.Id > 0)
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    //主表
                    repository.Add(subjectModel);
                    //节点表
                    repository.Add(subjectModel);
                    //节点详情表
                    repository.Add(subjectModel);
                    ts.Complete();//提交事务
                }
                
            }
            else
            {   //主表
                repository.Update(subjectModel);
                //节点表
                repository.Update(subjectModel);
                //节点详情表
                repository.Update(subjectModel);
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

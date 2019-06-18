using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class DepartmentBusiness : Business<Department>
    {
        public DepartmentBusiness(IServiceProvider provider, DepartmentRepository repository) : base(provider, repository)
        {

        }
        /// <summary>
        /// 验证Model完整性
        /// </summary>
        /// <param name="model">Department</param>
        /// <returns></returns>
        private Result Check(Department model)
        {
            if (string.IsNullOrWhiteSpace(model.DptName))
            {
                return Result.Fail("部门名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.DptCode))
            {
                return Result.Fail("部门编号不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.DptPositions))
            {
                return Result.Fail("部门正职不能为空");
            }
            if (model.Id == 0)
            {
                return base.GetSingle(p => p.DptName == model.DptName) != null ? Result.Fail("部门名称已经存在，无法重复添加！") : Result.Success();
            }
            if (model.Id != 0)
            {
                return base.GetSingle(p => p.DptName == model.DptName && p.Id != model.Id) != null ? Result.Fail("部门名称已经存在，无法执行本操作！") : Result.Success();
            }
            return Result.Success();
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="model">Department</param>
        /// <returns></returns>
        public override Result Add(Department model)
        {
            var result = Check(model);
            return result.Succeed ? base.Add(model) : result;
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="model">Department</param>
        /// <returns></returns>
        public override Result Update(Department model)
        {
            var result = Check(model);
            return result.Succeed ? base.Update(model) : result;
        }
        /// <summary>
        /// 删除部门（假删除）
        /// </summary>
        /// <param name="Id">部门ID</param>
        /// <returns></returns>
        public override Result Delete(int Id)
        {
            return this.Exist(p => p.HigherLevel == Id) ? Result.Fail("该部门含有下级，无法删除！") : base.Delete(Id);
        }
        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public override bool Exist(Expression<Func<Department, bool>> where)
        {
            return base.Exist(where);
        }
    }
}

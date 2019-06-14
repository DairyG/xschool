using System;
using XSchool.Businesses;
using XSchool.Core;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories;

namespace XShop.GCenter.Businesses
{
    public class DepartmentBusiness: Business<Department>
    {
        public DepartmentBusiness(IServiceProvider provider, DepartmentRepository repository) : base(provider, repository)
        {
            
        }
        private static Result Check(Department model)
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
            return Result.Success();
        }
        public override Result Add(Department model)
        {
            var result = Check(model);
            return result.Succeed ? base.Add(model) : result;
        }
        public override Result Update(Department model)
        {
            var result = Check(model);
            return result.Succeed ? base.Update(model) : result;
        }
        public override Result Delete(int Id)
        {
            return base.Delete(Id);
        }
    }
}

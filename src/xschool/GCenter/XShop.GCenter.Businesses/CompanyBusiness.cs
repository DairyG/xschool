using System;
using XSchool.Businesses;
using XSchool.Core;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories;

namespace XShop.GCenter.Businesses
{
    public class CompanyBusiness : Business<Company>
    {
        
        public CompanyBusiness(IServiceProvider provider, CompanyRepository repository) : base(provider, repository) { }
        
        public Result Check(Company model)
        {
            if (string.IsNullOrWhiteSpace(model.CompanyName))
            {
                return Result.Fail("公司名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Credit))
            {
                return Result.Fail("信用代码不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.LegalPerson))
            {
                return Result.Fail("法人代表不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Responsible))
            {
                return Result.Fail("公司负责人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.ResponsiblePhone))
            {
                return Result.Fail("负责人电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.BusinessDate))
            {
                return Result.Fail("营业期限不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.BusinessScope))
            {
                return Result.Fail("营业范围不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.CompanyPhone))
            {
                return Result.Fail("公司电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.OfficeAddress))
            {
                return Result.Fail("办公地址不能为空");
            }

            if (model.Id <= 0)
            {
                if (base.Exist(p => p.CompanyName.Equals(model.CompanyName) && p.Status.Equals(1)))
                {
                    return Result.Fail("公司名称已存在");
                }
                if (base.Exist(p => p.Credit.Equals(model.Credit) && p.Status.Equals(1)))
                {
                    return Result.Fail("信用代码已存在");
                }
                //var nameCount = base.Count(p => p.CompanyName.Equals(model.CompanyName));
                //if (nameCount > 0)
                //{
                //    return Result.Fail("公司名称已存在");
                //}
                //var creditCount = base.Count(p => p.Credit.Equals(model.Credit));
                //if (creditCount > 0)
                //{
                //    return Result.Fail("信用代码已存在");
                //}
            }
            else
            {
                if (base.Exist(p => p.CompanyName.Equals(model.CompanyName) && p.Id != model.Id && p.Status.Equals(1)))
                {
                    return Result.Fail("公司名称已存在");
                }
                if (base.Exist(p => p.Credit.Equals(model.Credit) && p.Id != model.Id && p.Status.Equals(1)))
                {
                    return Result.Fail("信用代码已存在");
                }

                //var nameCount = base.Count(p => p.Id != model.Id && p.CompanyName.Equals(model.CompanyName));
                //if (nameCount > 0)
                //{
                //    return Result.Fail("公司名称已存在");
                //}
                //var creditCount = base.Count(p => p.Id != model.Id && p.Credit.Equals(model.Credit));
                //if (creditCount > 0)
                //{
                //    return Result.Fail("信用代码已存在");
                //}
            }

            return Result.Success();
        }

        public Result AddOrEdit(Company model)
        {
            var result = Check(model);
            //新增
            if (model.Id <= 0)
            {
                return result.Succeed ? base.Add(model) : result;
            }
            else
            {
                return result.Succeed ? base.Update(model) : result;
            }
        }

        public Result Del(int id)
        {
            var model = base.GetSingle(id);
            if (model == null)
            {
                return Result.Fail("未找到数据");
            }
            model.Status = 0;
            return base.Update(model);
        }
    }
}

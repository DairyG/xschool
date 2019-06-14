using System;
using XSchool.Businesses;
using XSchool.Core;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories;

namespace XShop.GCenter.Businesses
{
    public class BankInfoBusiness : Business<BankInfo>
    {
        public BankInfoBusiness(IServiceProvider provider, BankInfoRepository repository) : base(provider, repository) { }

        private static Result Check(BankInfo model)
        {
            if (model.CompanyId == 0)
            {
                return Result.Fail("请先填写基本信息栏");
            }
            if (string.IsNullOrWhiteSpace(model.OpenBank))
            {
                return Result.Fail("开户银行不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.OpenBankName))
            {
                return Result.Fail("开户名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.BankAccount))
            {
                return Result.Fail("银行账号不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.LinkPhone))
            {
                return Result.Fail("联系电话不能为空");
            }
            return Result.Success();
        }

        public Result AddOrEdit(BankInfo model)
        {
            var result = Check(model);
            //新增
            if (model.Id == 0)
            {
                return result.Succeed ? base.Add(model) : result;
            }
            else
            {
                return result.Succeed ? base.Update(model) : result;
            }
        }
    }
}

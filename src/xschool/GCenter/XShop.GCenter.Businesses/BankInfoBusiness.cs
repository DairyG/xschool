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

        private Result Check(BankInfo model)
        {
            if (model.CompanyId <= 0)
            {
                return Result.Fail("请先填写基本信息");
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

            if (model.Id <= 0)
            {
                var accountCount = base.Count(p => p.BankAccount.Equals(model.BankAccount));
                if (accountCount > 0)
                {
                    return Result.Fail("银行账号已存在");
                }
            }
            else
            {
                var accountCount = base.Count(p => p.Id != model.Id && p.BankAccount.Equals(model.BankAccount));
                if (accountCount > 0)
                {
                    return Result.Fail("银行账号已存在");
                }
            }

            return Result.Success();
        }

        public Result AddOrEdit(BankInfo model)
        {
            var result = Check(model);

            //验证是否存在基本账户
            var basicCount = base.Count(p => p.IsDelete == 1 && p.CompanyId.Equals(model.CompanyId) && p.AccountType.Equals("基本账户"));
            if (basicCount > 0)
            {
                model.AccountType = "一般账户";
            }
            else
            {
                model.AccountType = "基本账户";
            }

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
            model.IsDelete = 0;
            return base.Update(model);
        }
    }
}

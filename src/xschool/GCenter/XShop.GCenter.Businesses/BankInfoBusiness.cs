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
                if (base.Exist(p => p.BankAccount.Equals(model.BankAccount) && p.Status.Equals(Status.Valid)))
                {
                    return Result.Fail("银行账号已存在");
                }
            }
            else
            {
                if (base.Exist(p => p.BankAccount.Equals(model.BankAccount) && p.Id != model.Id && p.Status.Equals(Status.Valid)))
                {
                    return Result.Fail("银行账号已存在");
                }
            }

            return Result.Success();
        }

        public Result AddOrEdit(BankInfo model)
        {
            var result = Check(model);
            if (!base.Exist(p => p.Status.Equals(1) && p.CompanyId.Equals(model.CompanyId) && p.Id != model.Id && p.AccountType.Equals(AccountType.Basic)))
            {
                model.AccountType = AccountType.Basic;
            }
            else
            {
                model.AccountType = AccountType.Normal;
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
            model.Status = 0;
            return base.Update(model);
        }
    }
}

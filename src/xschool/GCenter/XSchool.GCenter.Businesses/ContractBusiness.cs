using System;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;


namespace XSchool.GCenter.Businesses
{
    public class ContractBusiness : Business<Contract>
    {
        public ContractBusiness (IServiceProvider provider, ContractRepository repository) : base(provider, repository) { }

        private Result Check(Contract model)
        {
            if (string.IsNullOrWhiteSpace(model.No))
            {
                return Result.Fail("合同编号不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return Result.Fail("合同标题不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.StartTime.ToString()) || string.IsNullOrWhiteSpace(model.EndTime.ToString()))
            {
                return Result.Fail("错误的合同有效期");
            }
            if (model.Amount <= 0)
            {
                return Result.Fail("合同总金额错误");
            }
            if (string.IsNullOrWhiteSpace(model.AmountStr))
            {
                return Result.Fail("总金额大写不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.RelationNo))
            {
                return Result.Fail("请款单号不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaName))
            {
                return Result.Fail("甲方名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaAddr))
            {
                return Result.Fail("甲方地址不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaContact))
            {
                return Result.Fail("甲方联系人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaTel))
            {
                return Result.Fail("甲方联系电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaPerson))
            {
                return Result.Fail("甲方签字人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JiaSignDate.ToString()))
            {
                return Result.Fail("甲方签字时间不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiName))
            {
                return Result.Fail("乙方名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiAddr))
            {
                return Result.Fail("乙方地址不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiContact))
            {
                return Result.Fail("乙方联系人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiTel))
            {
                return Result.Fail("乙方联系电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiPerson))
            {
                return Result.Fail("乙方签字人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.YiSignDate.ToString()))
            {
                return Result.Fail("乙方签字时间不能为空");
            }
            if (model.Invoice == (int)IsInvoice.Yes) {
                if (string.IsNullOrWhiteSpace(model.InvoiceTitle))
                {
                    return Result.Fail("发票抬头不能为空");
                }
                if (model.InvoiceToType == (int)InvoiceToType.Company) {
                    if(model.InvoiceType == (int)InvoiceType.Normal || model.InvoiceType == (int)InvoiceType.Given)
                    {
                        if (string.IsNullOrWhiteSpace(model.InvoiceTaxNo))
                        {
                            return Result.Fail("税务登记证号不能为空");
                        }
                    }
                    if (model.InvoiceType == (int)InvoiceType.Given)
                    {
                        if (string.IsNullOrWhiteSpace(model.InvoiceBank))
                        {
                            return Result.Fail("基本户银行不能为空");
                        }
                        if (string.IsNullOrWhiteSpace(model.InvoiceBankNo))
                        {
                            return Result.Fail("基本户银行账号不能为空");
                        }
                        if (string.IsNullOrWhiteSpace(model.InvoiceTel))
                        {
                            return Result.Fail("注册固定电话不能为空");
                        }
                        if (string.IsNullOrWhiteSpace(model.InvoiceAddr))
                        {
                            return Result.Fail("注册场所地址不能为空");
                        }
                    }
                }
            }
            return Result.Success();
        }

        public Result AddOrEdit(Contract model)
        {
            var result = Check(model);

            //新增
            if (model.Id <= 0)
            {
                model.AddTime = DateTime.Now;
                return result.Succeed ? base.Add(model) : result;
            }
            else
            {
                return result.Succeed ? base.Update(model) : result;
            }
        }

    }
}

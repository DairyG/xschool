using System.Collections.Generic;
using XSchool.Core;
using XSchool.DoMain;
using XSchool.Helpers;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model.ViewModel;

namespace XShop.GCenter.DoMain
{
    public class CompanyDo : DoMainBase
    {
        private CompanyBusiness _companyBuiness;
        private BankInfoBusiness _bankInfoBusiness;

        public CompanyDo(CompanyBusiness companyBuiness, BankInfoBusiness bankInfoBusiness)
        {
            _bankInfoBusiness = bankInfoBusiness;
            _companyBuiness = companyBuiness;
        }

        public Result GetInfo(int id, bool isLoadBankInfo = false)
        {
            if (id <= 0)
            {
                return Result.Fail("主键编号不能小于等于零");
            }
            var model = _companyBuiness.GetSingle(p => p.Id.Equals(id)).MapTo<CompanyDto>();
            if (model == null)
            {
                return Result.Fail("未找到数据");
            }
            model.Bank = new List<Model.BankInfo>();
            if (isLoadBankInfo)
            {
                model.Bank = _bankInfoBusiness.Query(p => p.CompanyId.Equals(id));
            }
            return Result.Success(model);
        }
    }
}

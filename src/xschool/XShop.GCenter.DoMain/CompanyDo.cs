using XSchool.Core;
using XSchool.DoMain;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;

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

        public Company GetInfo(int id, bool isLoadBankInfo = false)
        {
            var model = _companyBuiness.GetSingle(p => p.Id.Equals(id));
            if (model == null)
            {
                return null;
            }
            if (isLoadBankInfo)
            {
                model.Bank = _bankInfoBusiness.Query(p => p.CompanyId.Equals(id) && p.Status.Equals(Status.Valid));
            }
            return model;
        }
    }
}
